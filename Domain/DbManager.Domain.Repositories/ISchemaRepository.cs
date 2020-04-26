using System.Collections.Generic;
using System.Threading.Tasks;
using DbManager.Domain.Models;

namespace DbManager.Domain.Repositories
{
    /// <summary>
    /// Repository for loading schema from database instance.
    /// </summary>
    public interface ISchemaRepository
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