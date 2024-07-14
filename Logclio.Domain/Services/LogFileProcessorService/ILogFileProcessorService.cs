using Logclio.Domain.Models;

namespace Logclio.Domain.Services.LogFileProcessorService
{
    public interface ILogFileProcessorService
    {
        /// <summary>
        /// Processes the log file and stores the log entries in the log store.
        /// </summary>
        /// <param name="logFile">The log file to process.</param>
        public Task ProcessLogFile(LogFile logFile);
    }
}
