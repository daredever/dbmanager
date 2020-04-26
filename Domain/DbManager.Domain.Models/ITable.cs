namespace DbManager.Domain.Models
{
    public interface ITable
    {
        string Catalog { get; }
        string Schema { get; }
        string Name { get; }
    }
}