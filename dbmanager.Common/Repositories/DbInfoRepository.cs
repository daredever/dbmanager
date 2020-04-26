using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dbmanager.Common.Models;
using System.Data.SqlClient;
using System.Linq;
using dbmanager.Common.Services;

namespace dbmanager.Common.Repositories
{
    public class DbInfoRepository : IDbInfoRepository
    {
        private readonly IHttpContextService _httpContextService;

        public DbInfoRepository(IHttpContextService httpContextService)
        {
            _httpContextService = httpContextService ?? throw new ArgumentNullException(nameof(httpContextService));
        }

        public async Task<IEnumerable<Catalog>> GetCatalogsAsync()
        {
            var query =
                @$"
                SELECT 
                    name as [{nameof(Catalog.Name)}]
                FROM sys.databases;
            ";

            var catalogs = new List<Catalog>();
            using (var connection = new SqlConnection(_httpContextService.DBConnectionString))
            {
                await connection.OpenAsync();

                using var command = new SqlCommand(query, connection);
                using var dataReader = await command.ExecuteReaderAsync();
                if (dataReader.HasRows)
                {
                    while (await dataReader.ReadAsync())
                    {
                        catalogs.Add(new Catalog {Name = dataReader.GetString(0)});
                    }
                }

                await dataReader.CloseAsync();
            }

            return catalogs.OrderBy(c => c.Name);
        }

        public async Task<IEnumerable<Table>> GetTablesAsync(Catalog catalog)
        {
            var catalogNameParameter = $"@{nameof(Table.Catalog)}";
            var query =
                @$"
                SELECT
                    TABLE_CATALOG AS [{nameof(Table.Catalog)}],
                    TABLE_SCHEMA AS [{nameof(Table.Schema)}],
                    TABLE_NAME AS [{nameof(Table.Name)}],
                    TABLE_TYPE AS [{nameof(Table.Type)}]
                FROM INFORMATION_SCHEMA.TABLES
                WHERE TABLE_CATALOG = {catalogNameParameter};
            ";

            var tables = new List<Table>();

            using (var connection = new SqlConnection(_httpContextService.DBConnectionString))
            {
                await connection.OpenAsync();
                await connection.ChangeDatabaseAsync(catalog.Name);

                using var command = new SqlCommand(query, connection);
                command.Parameters.Add(new SqlParameter(catalogNameParameter, catalog.Name));

                using var dataReader = await command.ExecuteReaderAsync();
                if (dataReader.HasRows)
                {
                    while (await dataReader.ReadAsync())
                    {
                        var table = new Table
                        {
                            Catalog = dataReader.GetString(0),
                            Schema = dataReader.GetString(1),
                            Name = dataReader.GetString(2),
                            Type = dataReader.GetString(3)
                        };

                        tables.Add(table);
                    }
                }

                await dataReader.CloseAsync();
            }

            return tables.OrderBy(c => c.Schema).ThenBy(c => c.Name);
        }

        public async Task<IEnumerable<Column>> GetColumnsAsync(Table table)
        {
            var catalogNameParameter = $"@{nameof(Table.Catalog)}";
            var schemaNameParameter = $"@{nameof(Table.Schema)}";
            var tableNameParameter = $"@{nameof(Table.Name)}";
            var query =
                $@"
                SELECT
                    TABLE_CATALOG AS [{nameof(Column.Catalog)}],
                    TABLE_SCHEMA AS [{nameof(Column.Schema)}],
                    COLUMN_NAME AS [{nameof(Column.Name)}],
                    DATA_TYPE AS [{nameof(Column.Type)}],
                    IS_NULLABLE AS [{nameof(Column.IsNullable)}],
                    CHARACTER_MAXIMUM_LENGTH AS [{nameof(Column.CharacterMaximumLength)}]
                FROM INFORMATION_SCHEMA.COLUMNS
                WHERE
                    TABLE_CATALOG = {catalogNameParameter}
                    AND TABLE_SCHEMA = {schemaNameParameter}
                    AND TABLE_NAME = {tableNameParameter};
            ";

            var columns = new List<Column>();

            using (var connection = new SqlConnection(_httpContextService.DBConnectionString))
            {
                await connection.OpenAsync();
                await connection.ChangeDatabaseAsync(table.Catalog);

                using var command = new SqlCommand(query, connection);
                command.Parameters.Add(new SqlParameter(catalogNameParameter, table.Catalog));
                command.Parameters.Add(new SqlParameter(schemaNameParameter, table.Schema));
                command.Parameters.Add(new SqlParameter(tableNameParameter, table.Name));

                using var dataReader = await command.ExecuteReaderAsync();
                if (dataReader.HasRows)
                {
                    while (await dataReader.ReadAsync())
                    {
                        var column = new Column
                        {
                            Catalog = dataReader.GetString(0),
                            Schema = dataReader.GetString(1),
                            Name = dataReader.GetString(2),
                            Type = dataReader.GetString(3),
                            IsNullable = dataReader.GetString(4),
                            CharacterMaximumLength = dataReader[5] as int?
                        };

                        columns.Add(column);
                    }
                }

                await dataReader.CloseAsync();
            }

            return columns.OrderBy(c => c.Name);
        }
    }
}