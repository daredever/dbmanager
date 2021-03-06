﻿namespace DbManager.Domain.Services
{
    /// <summary>
    /// Service for working with user session.
    /// </summary>
    public interface IUserContextService
    {
        /// <summary>
        /// Connection string to database instance.
        /// </summary>
        string DbConnectionString { get; set; }
    }
}