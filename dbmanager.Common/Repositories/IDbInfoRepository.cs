using System.Collections.Generic;
using System.Threading.Tasks;
using dbmanager.Common.Models;

namespace dbmanager.Common.Repositories
{
    public interface IDbInfoRepository
    {
        Task<IEnumerable<Catalog>> GetCatalogsAsync();
        Task<IEnumerable<Table>> GetTablesAsync(Catalog catalog);
        Task<IEnumerable<Column>> GetColumnsAsync(Table table);
    }
}