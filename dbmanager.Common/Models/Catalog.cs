using System;

namespace dbmanager.Common.Models
{
    /// <summary>
    /// Database item
    /// </summary>
    public class Catalog
    {
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}