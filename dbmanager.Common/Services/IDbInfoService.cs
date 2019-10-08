using System.Collections.Generic;
using System.Threading.Tasks;
using dbmanager.Common.Models;

namespace dbmanager.Common.Services
{
    public interface IDbInfoService
    {
        Task<IEnumerable<Catalog>> GetCatalogsAsync();
        Task<IEnumerable<Table>> GetTablesAsync(string catalog);
        Task<IEnumerable<Column>> GetColumnsAsync(string catalog, string schema, string table);
    }
}