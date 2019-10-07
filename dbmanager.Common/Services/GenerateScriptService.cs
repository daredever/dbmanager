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
            var columnsFullStr = string.Join("," + Environment.NewLine, columns.Select(c => c.ToString()));
            return $"CREATE TABLE {tableFullStr} ({Environment.NewLine + columnsFullStr + Environment.NewLine});";
        }

        public void SetConnectionString(string connectionString)
        {
            _repo.ConnectionString = connectionString;
        }
    }
}
