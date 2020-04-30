using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using DbManager.Domain.Models;
using DbManager.Domain.Models.DefaultImpl;
using DbManager.Domain.Repositories;
using DbManager.Domain.Services;

namespace DbManager.Infra.SqlServerRepos
{
    internal sealed class MsSqlSchemaRepository : ISchemaRepository
    {
        private readonly IUserContextService _userContextService;

        public MsSqlSchemaRepository(IUserContextService userContextService)
        {
            _userContextService = userContextService ?? throw new ArgumentNullException(nameof(userContextService));
        }

        public async Task<IReadOnlyCollection<ICatalog>> GetCatalogsAsync()
        {
            var query =
                $@"
                SELECT 
                    name as [{nameof(ICatalog.Name)}]
                FROM sys.databases;
            ";

            var catalogs = new List<ICatalog>();
            await using var connection = new SqlConnection(_userContextService.DbConnectionString);
            await connection.OpenAsync();

            await using var command = new SqlCommand(query, connection);
            await using var dataReader = await command.ExecuteReaderAsync();
            if (dataReader.HasRows)
            {
                while (await dataReader.ReadAsync())
                {
                    var catalog = new Catalog
                    {
                        Name = dataReader.GetString(0)
                    };

                    catalogs.Add(catalog);
                }
            }

            await dataReader.CloseAsync();

            return catalogs.OrderBy(c => c.Name).ToList();
        }

        public async Task<IReadOnlyCollection<ITable>> GetTablesAsync(ICatalog catalog)
        {
            var catalogNameParameter = $"@{nameof(ICatalog.Name)}";
            var query =
                $@"
                SELECT
                    TABLE_CATALOG AS [{nameof(ITable.Catalog)}],
                    TABLE_SCHEMA AS [{nameof(ITable.Schema)}],
                    TABLE_NAME AS [{nameof(ITable.Name)}]
                FROM INFORMATION_SCHEMA.TABLES
                WHERE TABLE_CATALOG = {catalogNameParameter};
            ";

            var tables = new List<ITable>();

            await using var connection = new SqlConnection(_userContextService.DbConnectionString);
            await connection.OpenAsync();
            await connection.ChangeDatabaseAsync(catalog.Name);

            await using var command = new SqlCommand(query, connection);
            command.Parameters.Add(new SqlParameter(catalogNameParameter, catalog.Name));

            await using var dataReader = await command.ExecuteReaderAsync();
            if (dataReader.HasRows)
            {
                while (await dataReader.ReadAsync())
                {
                    var table = new Table
                    {
                        Catalog = dataReader.GetString(0),
                        Schema = dataReader.GetString(1),
                        Name = dataReader.GetString(2)
                    };

                    tables.Add(table);
                }
            }

            await dataReader.CloseAsync();

            return tables.OrderBy(c => c.Schema).ThenBy(c => c.Name).ToList();
        }

        public async Task<IReadOnlyCollection<IColumn>> GetColumnsAsync(ITable table)
        {
            var catalogNameParameter = $"@{nameof(ITable.Catalog)}";
            var schemaNameParameter = $"@{nameof(ITable.Schema)}";
            var tableNameParameter = $"@{nameof(ITable.Name)}";
            var query =
                $@"
                SELECT
                    TABLE_CATALOG AS [{nameof(IColumn.Catalog)}],
                    TABLE_SCHEMA AS [{nameof(IColumn.Schema)}],
                    COLUMN_NAME AS [{nameof(IColumn.Name)}],
                    DATA_TYPE AS [{nameof(IColumn.Type)}],
                    IS_NULLABLE AS [{nameof(IColumn.IsNullable)}],
                    CHARACTER_MAXIMUM_LENGTH AS [{nameof(IColumn.CharactersMaxLength)}]
                FROM INFORMATION_SCHEMA.COLUMNS
                WHERE
                    TABLE_CATALOG = {catalogNameParameter}
                    AND TABLE_SCHEMA = {schemaNameParameter}
                    AND TABLE_NAME = {tableNameParameter};
            ";

            var columns = new List<IColumn>();

            await using var connection = new SqlConnection(_userContextService.DbConnectionString);
            await connection.OpenAsync();
            await connection.ChangeDatabaseAsync(table.Catalog);

            await using var command = new SqlCommand(query, connection);
            command.Parameters.Add(new SqlParameter(catalogNameParameter, table.Catalog));
            command.Parameters.Add(new SqlParameter(schemaNameParameter, table.Schema));
            command.Parameters.Add(new SqlParameter(tableNameParameter, table.Name));

            await using var dataReader = await command.ExecuteReaderAsync();
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
                        CharactersMaxLength = dataReader[5] as int?
                    };

                    columns.Add(column);
                }
            }

            await dataReader.CloseAsync();

            return columns.OrderBy(c => c.Name).ToList();
        }
    }
}