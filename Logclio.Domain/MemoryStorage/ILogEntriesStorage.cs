using Logclio.Domain.Models;

namespace Logclio.Domain.MemoryStorage
{
    /// <summary>
    /// Singleton service for in-memory log entries storage and search functionality.
    /// </summary>
    public interface ILogEntriesStorage
    {
        /// <summary>
        /// Adds a log entry to the in-memory store.
        /// </summary>
        /// <param name="logEntry">The log entry to add.</param>
        void Add(LogEntry logEntry);

        /// <summary>
        /// Get all log entries.
        /// </summary>
        /// <returns>A queryable collection of all log entries.</returns>
        public IQueryable<LogEntry> GetAllAsQueryable();
    }
}
