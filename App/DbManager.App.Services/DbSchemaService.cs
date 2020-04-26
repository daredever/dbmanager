using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DbManager.Domain.Models;
using DbManager.Domain.Repositories;
using DbManager.Domain.Services;

namespace DbManager.App.Services
{
    public class DbSchemaService : IDbSchemaService
    {
        private readonly ISchemaRepository _schemaRepository;

        public DbSchemaService(ISchemaRepository schemaRepository)
        {
            _schemaRepository = schemaRepository ?? throw new ArgumentNullException(nameof(schemaRepository));
        }

        public Task<IReadOnlyCollection<ICatalog>> GetCatalogsAsync()
        {
            return _schemaRepository.GetCatalogsAsync();
        }

        public Task<IReadOnlyCollection<ITable>> GetTablesAsync(ICatalog catalog)
        {
            return _schemaRepository.GetTablesAsync(catalog);
        }

        public Task<IReadOnlyCollection<IColumn>> GetColumnsAsync(ITable table)
        {
            return _schemaRepository.GetColumnsAsync(table);
        }
    }
}