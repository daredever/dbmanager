using System;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace dbmanager.Common.Services
{
    public class HttpContextService : IHttpContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        private const string ConnectionStringKey = "db_connection_string";

        public HttpContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public string DBConnectionString
        {
            get
            {
                // TODO change to GetString(key) 
                _session.TryGetValue(ConnectionStringKey, out byte[] bytes);
                return Encoding.UTF8.GetString(bytes);
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    // TODO change to SetString(key, value) 
                    _session.Set(ConnectionStringKey, Encoding.UTF8.GetBytes(value));
                }
            }
        }
    }
}
