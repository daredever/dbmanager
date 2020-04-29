using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DbManager.Domain.Models;
using DbManager.Domain.Repositories;
using DbManager.Domain.Services;

namespace DbManager.App.Services
{
    internal sealed class DbScriptsService : IDbScriptsService
    {
        private readonly ISchemaRepository _schemaRepository;

        public DbScriptsService(ISchemaRepository schemaRepository)
        {
            _schemaRepository = schemaRepository ?? throw new ArgumentNullException(nameof(schemaRepository));
        }

        public async Task<string> GenerateCreateTableScriptAsync(ITable table)
        {
            var columns = await _schemaRepository.GetColumnsAsync(table);
            return $"CREATE TABLE {table.Schema}.{table.Name} (" +
                   $"\r\n{GenerateColumnsScriptPart(columns)}\r\n" +
                   ");";
        }

        private static string GenerateColumnsScriptPart(IEnumerable<IColumn> columns)
        {
            var sb = new StringBuilder();
            var separator = ",\r\n";

            var firstColumn = true;
            foreach (var column in columns)
            {
                if (firstColumn != true)
                {
                    sb.Append(separator);
                }

                firstColumn = false;

                sb.Append(new string(' ', 4));
                sb.Append(column.Name);
                sb.Append(column.CharactersMaxLength != null
                    ? $" {column.Type}({column.CharactersMaxLength})"
                    : $" {column.Type}");

                if (column.IsNullable.Equals("NO", StringComparison.OrdinalIgnoreCase))
                {
                    sb.Append(" NOT NULL");
                }
            }

            return sb.ToString();
        }
    }
}