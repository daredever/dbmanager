using System;
using System.Text;

namespace dbmanager.Common.Models
{
    public class Column : DBObject
    {
        public string IsNullable { get; set; }
        public int? CharacterMaximumLength { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append(Name);

            sb.Append(CharacterMaximumLength != null ? $" {Type}({CharacterMaximumLength})" : $" {Type}");

            if (IsNullable.Equals("NO", StringComparison.OrdinalIgnoreCase))
            {
                sb.Append(" NOT NULL");
            }

            return sb.ToString();
        }
    }
}
