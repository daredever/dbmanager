using System;
using System.Linq;
using System.Threading.Tasks;
using dbmanager.Common.Models;
using dbmanager.Common.Repositories;

namespace dbmanager.Common.Services
{
    public class GenerateScriptService : IGenerateScriptService
    {
        private readonly IDbInfoRepository _repo;

        public GenerateScriptService(IDbInfoRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        public async Task<string> GetCreateTableScriptAsync(string catalog, string schema, string tableName)
        {
            var table = new Table { Catalog = catalog, Schema = schema, Name = tableName };
            var tableFullStr = table.ToString();
            var columns = await _repo.GetColumnsAsync(table);
            var newLine = Environment.NewLine;
            var columnsFullStr = string.Format("{0}{1}{0}",
                newLine,
                string.Join($",{newLine}", columns.Select(c => $"\t{c.ToString()}")));

            return $"CREATE TABLE {tableFullStr} ({columnsFullStr});";
        }

        public void SetConnectionString(string connectionString)
        {
            _repo.ConnectionString = connectionString;
        }
    }
}
