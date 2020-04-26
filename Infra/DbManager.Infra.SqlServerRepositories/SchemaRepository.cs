using System.Collections.Generic;
using System.Threading.Tasks;
using DbManager.Domain.Models;
using DbManager.Domain.Repositories;

namespace DbManager.Infra.SqlServerRepositories
{
    public class SchemaRepository : ISchemaRepository
    {
        public Task<IReadOnlyCollection<ICatalog>> GetCatalogsAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<IReadOnlyCollection<ITable>> GetTablesAsync(ICatalog catalog)
        {
            throw new System.NotImplementedException();
        }

        public Task<IReadOnlyCollection<IColumn>> GetColumnsAsync(ITable table)
        {
            throw new System.NotImplementedException();
        }
    }
}