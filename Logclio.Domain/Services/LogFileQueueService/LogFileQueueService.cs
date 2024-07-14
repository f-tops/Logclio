using Logclio.Domain.Models;
using System.Collections.Concurrent;

namespace Logclio.Domain.Services.LogFileQueueService
{
    public class LogFileQueueService : ILogFileQueueService
    {
        private readonly ConcurrentQueue<LogFile> _logFileProcessingQueue = new();

        public void EnqueueLogFile(LogFile logFile)
        {
            _logFileProcessingQueue.Enqueue(logFile);
        }

        public LogFile? DequeueLogFile()
        {
            return _logFileProcessingQueue.TryDequeue(out var nextLogFile) ? nextLogFile : null;
        }
    }
}
