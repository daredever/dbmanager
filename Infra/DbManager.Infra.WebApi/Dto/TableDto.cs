namespace DbManager.Infra.WebApi.Dto
{
    public sealed class TableDto
    {
        public string Catalog { get; set; }
        public string Schema { get; set; }
        public string Name { get; set; }
    }
}