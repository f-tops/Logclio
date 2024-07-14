using Logclio.Domain.Models;

namespace Logclio.Domain.Services.LogFileQueueService
{
    /// <summary>
    /// Provides functionality to enqueue and dequeue log files for processing.
    /// </summary>
    public interface ILogFileQueueService
    {
        /// <summary>
        /// Enqueues a log file for processing.
        /// </summary>
        /// <param name="logFile">The log file to enqueue.</param>
        public void EnqueueLogFile(LogFile logFile);

        /// <summary>
        /// Dequeues the next log file for processing.
        /// </summary>
        /// <returns>
        /// The next <see cref="LogFile"/> to be processed, or <c>null</c> if the queue is empty.
        /// </returns>
        public LogFile? DequeueLogFile();
    }
}
