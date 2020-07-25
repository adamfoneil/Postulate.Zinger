using System.Collections.Generic;

namespace Zinger.Models
{
    public class SavedQuery
    {
        /// <summary>
        /// Connection name from dropdown
        /// </summary>
        public string ConnectionName { get; set; }

        /// <summary>
        /// Becomes the class name and basis of result type name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// SQL content of query
        /// </summary>
        public string Sql { get; set; }

        public List<Parameter> Parameters { get; set; }
    }
}