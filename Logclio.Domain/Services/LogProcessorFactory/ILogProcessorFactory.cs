using Logclio.Domain.Services.LogFileProcessorService;

namespace Logclio.Domain.Services.LogProcessorFactory
{
    /// <summary>
    /// Factory class for creating log file processor services based on file extensions.
    /// </summary>
    public interface ILogProcessorFactory
    {
        /// <summary>
        /// Gets the log file processor service for the specified file extension.
        /// </summary>
        /// <param name="extension">The file extension to get the processor for.</param>
        /// <returns>The log file processor service for the specified file extension.</returns>
        /// <exception cref="NotSupportedException">Thrown when the file extension is not supported.</exception>
        public ILogFileProcessorService GetProcessor(string extension);
    }
}
