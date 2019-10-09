namespace dbmanager.Common.Services
{
    /// <summary>
    /// Service for working with user session
    /// </summary>
    public interface IHttpContextService
    {
        /// <summary>
        /// Connection string to database instance
        /// </summary>
        string DBConnectionString { get; set; }
    }
}