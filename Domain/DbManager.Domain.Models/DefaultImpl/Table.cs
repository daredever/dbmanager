namespace DbManager.Domain.Models.DefaultImpl
{
    public class Table : ITable
    {
        public string Catalog { get; set; }
        public string Schema { get; set; }
        public string Name { get; set; }
    }
}