namespace Logclio.Domain.Models
{
    /// <summary>
    /// Represents a single log entry.
    /// </summary>
    public class LogEntry
    {
        /// <summary>
        /// Gets or sets the timestamp of the log entry.
        /// </summary>
        /// <value>
        /// The date and time when the log entry was created.
        /// </value>
        public required DateTime Timestamp { get; set; }

        /// <summary>
        /// Gets or sets the log level of the log entry.
        /// </summary>
        /// <value>
        /// The severity level of the log entry (e.g., INFO, WARN, ERROR).
        /// </value>
        public required string LogLevel { get; set; }

        /// <summary>
        /// Gets or sets the instance ID where the log entry originated.
        /// </summary>
        /// <value>
        /// The identifier of the application instance that generated the log entry.
        /// </value>
        public required string InstanceId { get; set; }

        /// <summary>
        /// Gets or sets the module where the log entry was generated.
        /// </summary>
        /// <value>
        /// The name of the application module that generated the log entry.
        /// </value>
        public required string Module { get; set; }

        /// <summary>
        /// Gets or sets the message of the log entry.
        /// </summary>
        /// <value>
        /// The message describing the event or error that the log entry represents.
        /// </value>
        public required string Message { get; set; }
    }
}
