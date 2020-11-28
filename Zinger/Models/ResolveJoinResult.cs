using SqlSchema.Library.Models;
using System.Collections.Generic;
using System.Linq;

namespace Zinger.Models
{
    public class ResolveJoinResult
    {
        public bool IsSuccessful => ForeignKeys.Any();
        public string FromClause { get; set; }
        public IEnumerable<ForeignKey> ForeignKeys { get; set; }
        public IEnumerable<string> UnrecognziedAliases { get; set; }
    }
}
