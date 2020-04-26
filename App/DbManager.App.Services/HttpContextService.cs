using System;
using System.Text;
using DbManager.Domain.Services;
using Microsoft.AspNetCore.Http;

namespace DbManager.App.Services
{
    public class HttpContextService : IHttpContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string ConnectionStringKey = "db_connection_string";

        public HttpContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public string DbConnectionString
        {
            get
            {
                // TODO change to GetString(key) 
                if (Session.TryGetValue(ConnectionStringKey, out var bytes))
                {
                    return Encoding.UTF8.GetString(bytes);
                }

                throw new ArgumentException("Connection string is not specified");
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value) != true)
                {
                    // TODO change to SetString(key, value) 
                    Session.Set(ConnectionStringKey, Encoding.UTF8.GetBytes(value));
                }
            }
        }

        private ISession Session => _httpContextAccessor.HttpContext.Session;
    }
}