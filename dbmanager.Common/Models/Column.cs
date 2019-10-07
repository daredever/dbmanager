using System;
namespace dbmanager.Common.Models
{
    public class Column : DBObject
    {
        public string IsNullable { get; set; }
        public int? CharacterMaximumLength { get; set; }

        public override string ToString()
        {
            return $"{Name} {Type}";
        }
    }
}
