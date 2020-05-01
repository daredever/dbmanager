namespace DbManager.Infra.WebApi.Dto
{
    public sealed class ColumnDto
    {
        public string Catalog { get; set; }
        public string Schema { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string IsNullable { get; set; }
        public int? CharactersMaxLength { get; set; }
    }
}