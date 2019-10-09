using System.Collections.Generic;
using System.Threading.Tasks;
using dbmanager.Common.Models;

namespace dbmanager.Common.Services
{
    /// <summary>
    /// Represent data about DB instance schema 
    /// </summary>
    public interface IDbInfoService
    {
        /// <summary>
        /// Get databases list
        /// </summary>
        /// <returns>Databases list</returns>
        Task<IEnumerable<Catalog>> GetCatalogsAsync();

        /// <summary>
        /// Get table list for database
        /// </summary>
        /// <param name="catalog">Current database</param>
        /// <returns>Tables list</returns>
        Task<IEnumerable<Table>> GetTablesAsync(string catalog);

        /// <summary>
        /// Get columns list for table 
        /// </summary>
        /// <param name="catalog">Current database</param>
        /// <param name="schema">Current schema</param>
        /// <param name="table">Current table</param>
        /// <returns>Columns list</returns>
        Task<IEnumerable<Column>> GetColumnsAsync(string catalog, string schema, string table);
    }
}