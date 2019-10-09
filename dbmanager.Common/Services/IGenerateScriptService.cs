using System.Threading.Tasks;
using dbmanager.Common.Models;

namespace dbmanager.Common.Services
{
    /// <summary>
    /// Generates scripts for database objects
    /// </summary>
    public interface IGenerateScriptService
    {
        /// <summary>
        /// Generate create table script
        /// </summary>
        /// <param name="catalog">Current database</param>
        /// <param name="schema">Current schema</param>
        /// <param name="tableName">Current table</param>
        /// <returns>Create table script</returns>
        Task<string> GetCreateTableScriptAsync(string catalog, string schema, string tableName);
    }
}