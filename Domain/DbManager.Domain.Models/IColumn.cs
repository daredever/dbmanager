namespace DbManager.Domain.Models
{
    public interface IColumn
    {
        string Catalog { get; }
        string Schema { get; }
        string Name { get; }
        string Type { get; }
        string IsNullable { get; }
        int? CharactersMaxLength { get; }
    }
}