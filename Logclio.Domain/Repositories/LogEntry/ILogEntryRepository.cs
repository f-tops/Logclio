namespace Logclio.Domain.Repositories.LogEntry
{
    /// <summary>
    /// Repository for managing log entries.
    /// </summary>
    public interface ILogEntryRepository
    {
        /// <summary>
        /// Adds a log entry to the repository.
        /// </summary>
        /// <param name="logEntry">The log entry to add.</param>
        public void AddLogEntry(Models.LogEntry logEntry);

        /// <summary>
        /// Searches log entries based on the specified criteria.
        /// </summary>
        /// <param name="criteria">The search criteria for filtering log entries.</param>
        /// <returns>A collection of log entries that match the search criteria.</returns>
        public IEnumerable<Models.LogEntry> Search(Models.LogSearchCriteria criteria);
    }
}
