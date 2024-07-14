using Logclio.Domain.Models;
using System.Collections.Concurrent;

namespace Logclio.Domain.MemoryStorage
{
    public class LogEntriesStorage : ILogEntriesStorage
    {
        private readonly ConcurrentBag<LogEntry> _allLogEntries = [];

        public void Add(LogEntry logEntry)
        {
            _allLogEntries.Add(logEntry);
        }

        public IQueryable<LogEntry> GetAllAsQueryable()
        {
            return _allLogEntries.AsQueryable();
        }
    }
}
