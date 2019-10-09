using System;
using System.Collections.Generic;

namespace dbmanager.Common.Models
{
    /// <summary>
    /// Database's table
    /// </summary>
    public class Table : DBObject
    {
        public override string ToString()
        {
            return $"{Schema}.{Name}";
        }
    }
}
