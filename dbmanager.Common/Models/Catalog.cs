using System;
namespace dbmanager.Common.Models
{
    public class Catalog
    {
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
