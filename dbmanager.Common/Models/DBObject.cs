using System;
namespace dbmanager.Common.Models
{
    public class DBObject
    {
        public string Catalog { get; set; }
        public string Schema { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
