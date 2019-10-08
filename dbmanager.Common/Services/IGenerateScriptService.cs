using System.Threading.Tasks;
using dbmanager.Common.Models;

namespace dbmanager.Common.Services
{
    public interface IGenerateScriptService
    {
        Task<string> GetCreateTableScriptAsync(string catalog, string schema, string tableName);
    }
}