using System.Collections.Generic;
using System.Threading.Tasks;
using dbmanager.Common.Models;

namespace dbmanager.Common.Repositories
{
    /// <summary>
    /// Service for working with database instance
    /// </summary>
    public interface IDbInfoRepository
    {
        /// <summary>
        /// Load databases list
        /// </summary>
        /// <returns>Databases list</returns>
        Task<IEnumerable<Catalog>> GetCatalogsAsync();

        /// <summary>
        /// Load tables list
        /// </summary>
        /// <param name="catalog">Current database</param>
        /// <returns>Tables list</returns>
        Task<IEnumerable<Table>> GetTablesAsync(Catalog catalog);

        /// <summary>
        /// Load columns list
        /// </summary>
        /// <param name="table">Current table</param>
        /// <returns>Columns list</returns>
        Task<IEnumerable<Column>> GetColumnsAsync(Table table);
    }
}