using Logclio.Domain.MemoryStorage;

namespace Logclio.Domain.Repositories.LogEntry
{
    public class LogEntryRepository(
        ILogEntriesStorage logEntriesStorage) : ILogEntryRepository
    {
        public void AddLogEntry(Models.LogEntry logEntry)
        {
            logEntriesStorage.Add(logEntry);
        }

        public IEnumerable<Models.LogEntry> Search(Models.LogSearchCriteria criteria)
        {
            var query = logEntriesStorage.GetAllAsQueryable();

            if (criteria.Timestamp.HasValue)
            {
                query = query.Where(entry => entry.Timestamp == criteria.Timestamp.Value);
            }

            if (!string.IsNullOrWhiteSpace(criteria.LogLevel))
            {
                query = query.Where(entry => entry.LogLevel.Equals(criteria.LogLevel, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(criteria.InstanceId))
            {
                query = query.Where(entry => entry.InstanceId.Equals(criteria.InstanceId, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(criteria.Module))
            {
                query = query.Where(entry => entry.Module.Equals(criteria.Module, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(criteria.Message))
            {
                query = query.Where(entry => entry.Message.Contains(criteria.Message, StringComparison.OrdinalIgnoreCase));
            }

            return query.ToList();
        }
    }
}
