using DbManager.Domain.Models;
using DbManager.Domain.Models.DefaultImpl;

namespace DbManager.Infra.WebApi.Dto
{
    internal static class MapExtensions
    {
        public static ColumnDto Map(this IColumn model) =>
            new ColumnDto
            {
                Catalog = model.Catalog,
                Schema = model.Schema,
                Name = model.Name,
                Type = model.Type,
                IsNullable = model.IsNullable,
                CharactersMaxLength = model.CharactersMaxLength
            };

        public static IColumn Map(this ColumnDto dto) =>
            new Column
            {
                Catalog = dto.Catalog,
                Schema = dto.Schema,
                Name = dto.Name,
                Type = dto.Type,
                IsNullable = dto.IsNullable,
                CharactersMaxLength = dto.CharactersMaxLength
            };

        public static TableDto Map(this ITable model) =>
            new TableDto
            {
                Catalog = model.Catalog,
                Schema = model.Schema,
                Name = model.Name
            };

        public static ITable Map(this TableDto dto) =>
            new Table
            {
                Catalog = dto.Catalog,
                Schema = dto.Schema,
                Name = dto.Name
            };

        public static CatalogDto Map(this ICatalog model) =>
            new CatalogDto
            {
                Name = model.Name
            };

        public static ICatalog Map(this CatalogDto dto) =>
            new Catalog
            {
                Name = dto.Name
            };
    }
}