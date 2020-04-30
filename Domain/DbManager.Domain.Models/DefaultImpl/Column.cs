namespace DbManager.Domain.Models.DefaultImpl
{
    public class Column : IColumn
    {
        public string Catalog { get; set; }
        public string Schema { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string IsNullable { get; set; }
        public int? CharactersMaxLength { get; set; }
    }
}