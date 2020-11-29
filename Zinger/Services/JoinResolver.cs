using SqlSchema.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Zinger.Models;

namespace Zinger.Services
{
    public class JoinResolver
    {
        private readonly IEnumerable<DbObject> _objects;
        private readonly AliasManager _aliasManager;

        public JoinResolver(IEnumerable<DbObject> objects, AliasManager aliasManager)
        {
            _objects = objects;
            _aliasManager = aliasManager;
        }

        public ResolveJoinResult Execute(string aliasList)
        {
            var result = new ResolveJoinResult();

            var aliases = aliasList.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            // aliases that correspond to known tables
            var foundTables = from a in aliases
                              join am in _aliasManager.Aliases on a equals am.Key
                              join t in _objects on am.Value equals $"{t.Schema}.{t.Name}"
                              where t.Type == DbObjectType.Table
                              select new
                              {
                                  Alias = a,
                                  Table = t as Table
                              };

            // aliases that don't correspond to known tables
            result.UnrecognziedAliases = aliases.Except(_aliasManager.Aliases.Select(kp => kp.Key));

            // easy access to aliases when building join syntax
            var foundTablesByTable = foundTables.ToDictionary(item => item.Table);

            // easy access to tables by alias
            var foundTablesByAlias = foundTables.ToDictionary(item => item.Alias);

            // foreign keys enclosed by the found tables
            result.ForeignKeys = _objects.OfType<ForeignKey>().Where(fk => IsEnclosed(fk));

            // flattens overlapping FKs into a dictionary by a combo of left and right table aliases (default "Down" join direction)
            var fkDictionary = result.ForeignKeys
                .GroupBy(fk => $"{foundTablesByTable[fk.ReferencedTable].Alias}.{foundTablesByTable[fk.ReferencingTable].Alias}")
                .ToDictionary(grp => grp.Key, grp => grp.First());

            List<string> joins = new List<string>();
            HashSet<string> completedAliases = new HashSet<string>();

            joins.Add(TableSyntax(foundTablesByAlias[aliases[0]].Table, aliases[0]));
            completedAliases.Add(aliases[0]);

            for (int i = 1; i < aliases.Length; i++)
            {
                var fk = FindForeignKey(aliases[i], out JoinDirection joinDirection);
                joins.Add(JoinSyntax(aliases[i], fk, joinDirection));
                completedAliases.Add(aliases[i]);
            }

            result.FromClause = string.Join("\r\n", joins);

            return result;

            bool IsEnclosed(ForeignKey fk) => IsFound(fk.ReferencedTable) && IsFound(fk.ReferencingTable);

            bool IsFound(Table tbl) => foundTables.Any(t => t.Table.Equals(tbl));

            // find a foreign key comprised of the given alias and any of the completed aliases
            ForeignKey FindForeignKey(string rightAlias, out JoinDirection joinDirection)
            {
                foreach (var leftAlias in completedAliases)
                {
                    string key = $"{leftAlias}.{rightAlias}";
                    if (fkDictionary.ContainsKey(key))
                    {
                        joinDirection = JoinDirection.Down;
                        return fkDictionary[key];
                    }

                    // swap left and right and try again
                    key = $"{rightAlias}.{leftAlias}";
                    if (fkDictionary.ContainsKey(key))
                    {
                        joinDirection = JoinDirection.Up;
                        return fkDictionary[key];
                    }
                }

                throw new Exception($"Couldn't find a foreign key with table alias {rightAlias} among the completed aliases {string.Join(", ", completedAliases)}");
            }

            string JoinSyntax(string fromAlias, ForeignKey fk, JoinDirection joinDirection)
            {
                var fromTable = foundTablesByAlias[fromAlias].Table;
                string toAlias = null;

                string syntax = $"INNER JOIN {TableSyntax(fromTable, fromAlias)}";

                switch (joinDirection)
                {
                    case JoinDirection.Up:
                        var referencedTable = fk.ReferencedTable;
                        var referencedAlias = foundTablesByTable[referencedTable].Alias;
                        toAlias = foundTablesByTable[fk.ReferencingTable].Alias;
                        syntax += $" ON {string.Join(" AND ", fk.Columns.Select(col => $"[{referencedAlias}].[{col.ReferencedName}]=[{toAlias}].[{col.ReferencingName}]"))}";
                        break;

                    case JoinDirection.Down:
                        var referencingTable = fk.ReferencingTable;
                        var referencingAlias = foundTablesByTable[referencingTable].Alias;
                        toAlias = foundTablesByTable[fk.ReferencedTable].Alias;
                        syntax += $" ON {string.Join(" AND ", fk.Columns.Select(col => $"[{fromAlias}].[{col.ReferencingName}]=[{toAlias}].[{col.ReferencedName}]"))}";
                        break;
                }

                return syntax;
            }

            string TableSyntax(Table table, string alias) => $"[{table.Schema}].[{table.Name}] [{alias}]";
        }

        private enum JoinDirection
        {
            /// <summary>
            /// from referencing to referenced
            /// </summary>
            Up,
            /// <summary>
            /// from referenced to referencing
            /// </summary>
            Down
        }
    }
}
