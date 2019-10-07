using System;
using System.Collections.Generic;

namespace dbmanager.Common.Models
{
    public class Table : DBObject
    {
        public override string ToString()
        {
            return $"{Schema}.{Name}";
        }
    }
}
