using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dbmanager.Common.Models;
using System.Data.SqlClient;
using System.Linq;

namespace dbmanager.Common.Repositories
{
    public class DbInfoRepository : IDbInfoRepository
    {
        private string _connectionString;

        public string ConnectionString
        {
            get
            {
                return _connectionString;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _connectionString = value;
                }
            }
        }

        public async Task<IEnumerable<Catalog>> GetCatalogsAsync()
        {
            var query =
            @"
                SELECT 
                    name as [Name]
                FROM sys.databases;
            ";

            var catalogs = new List<Catalog>();
            using (var connection = new SqlConnection(ConnectionString))
            {
                await connection.OpenAsync();

                var command = new SqlCommand(query, connection);

                var dataReader = await command.ExecuteReaderAsync();
                if (dataReader.HasRows)
                {
                    while (await dataReader.ReadAsync())
                    {
                        catalogs.Add(new Catalog { Name = dataReader.GetString(0) });
                    }
                }

                dataReader.Close();
            }

            return catalogs.OrderBy(c => c.Name);
        }

        public async Task<IEnumerable<Table>> GetTablesAsync(Catalog catalog)
        {
            var catalogNameParameter = "@catalogName";
            var query =
            @$"
                SELECT
                    TABLE_CATALOG AS [Catalog],
                    TABLE_SCHEMA AS [Schema],
                    TABLE_NAME AS [Name],
                    TABLE_TYPE AS [Type]
                FROM INFORMATION_SCHEMA.TABLES
                WHERE TABLE_CATALOG = {catalogNameParameter};
            ";

            var tables = new List<Table>();

            using (var connection = new SqlConnection(ConnectionString))
            {
                await connection.OpenAsync();
                await connection.ChangeDatabaseAsync(catalog.Name);

                var command = new SqlCommand(query, connection);

                command.Parameters.Add(new SqlParameter(catalogNameParameter, catalog.Name));

                var dataReader = await command.ExecuteReaderAsync();
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

                dataReader.Close();
            }

            return tables.OrderBy(c => c.Schema).ThenBy(c => c.Name);
        }

        public async Task<IEnumerable<Column>> GetColumnsAsync(Table table)
        {
            var catalogNameParameter = "@catalogName";
            var schemaNameParameter = "@schemaName";
            var tableNameParameter = "@tableName";
            var query =
            $@"
                SELECT
                    TABLE_CATALOG AS [Catalog],
                    TABLE_SCHEMA AS [Schema],
                    COLUMN_NAME AS [Name],
                    DATA_TYPE AS [Type],
                    IS_NULLABLE AS [IsNullable],
                    CHARACTER_MAXIMUM_LENGTH AS [CharacterMaximumLength]
                FROM INFORMATION_SCHEMA.COLUMNS
                WHERE
                    TABLE_CATALOG = {catalogNameParameter}
                    AND TABLE_SCHEMA = {schemaNameParameter}
                    AND TABLE_NAME = {tableNameParameter};
            ";

            var columns = new List<Column>();

            using (var connection = new SqlConnection(ConnectionString))
            {
                await connection.OpenAsync();
                await connection.ChangeDatabaseAsync(table.Catalog);

                var command = new SqlCommand(query, connection);

                command.Parameters.Add(new SqlParameter(catalogNameParameter, table.Catalog));
                command.Parameters.Add(new SqlParameter(schemaNameParameter, table.Schema));
                command.Parameters.Add(new SqlParameter(tableNameParameter, table.Name));

                var dataReader = await command.ExecuteReaderAsync();
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

                dataReader.Close();
            }

            return columns.OrderBy(c => c.Name);
        }
    }
}
