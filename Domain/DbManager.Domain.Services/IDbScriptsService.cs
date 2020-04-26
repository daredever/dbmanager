using System.Threading.Tasks;
using DbManager.Domain.Models;

namespace DbManager.Domain.Services
{
    /// <summary>
    /// Generates scripts for database objects.
    /// </summary>
    public interface IDbScriptsService
    {
        /// <summary>
        /// Generates create table script.
        /// </summary>
        /// <param name="table">Table</param>
        /// <returns>Create table script</returns>
        Task<string> GenerateCreateTableScriptAsync(ITable table);
    }
}