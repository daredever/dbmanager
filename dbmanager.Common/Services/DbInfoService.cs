using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dbmanager.Common.Models;
using dbmanager.Common.Repositories;

namespace dbmanager.Common.Services
{
    public class DbInfoService : IDbInfoService
    {
        private readonly IDbInfoRepository _repo;

        public DbInfoService(IDbInfoRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        public Task<IEnumerable<Catalog>> GetCatalogsAsync()
        {
            return _repo.GetCatalogsAsync();
        }

        public Task<IEnumerable<Table>> GetTablesAsync(string catalog)
        {
            return _repo.GetTablesAsync(new Catalog {Name = catalog});
        }

        public Task<IEnumerable<Column>> GetColumnsAsync(string catalog, string schema, string table)
        {
            return _repo.GetColumnsAsync(new Table {Catalog = catalog, Schema = schema, Name = table});
        }
    }
}