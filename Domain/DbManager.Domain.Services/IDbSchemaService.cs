using System.Collections.Generic;
using System.Threading.Tasks;
using DbManager.Domain.Models;

namespace DbManager.Domain.Services
{
    /// <summary>
    /// Represent data about DB instance schema.
    /// </summary>
    public interface IDbSchemaService
    {
        /// <summary>
        /// Load catalogs at current db instance.
        /// </summary>
        /// <returns>Catalogs at db instance</returns>
        Task<IReadOnlyCollection<ICatalog>> GetCatalogsAsync();

        /// <summary>
        /// Load tables at catalog.
        /// </summary>
        /// <param name="catalog">Current catalog</param>
        /// <returns>Tables</returns>
        Task<IReadOnlyCollection<ITable>> GetTablesAsync(ICatalog catalog);

        /// <summary>
        /// Load columns at table.
        /// </summary>
        /// <param name="table">Current table</param>
        /// <returns>Columns</returns>
        Task<IReadOnlyCollection<IColumn>> GetColumnsAsync(ITable table);
    }
}