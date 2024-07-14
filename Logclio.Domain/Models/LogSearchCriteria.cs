namespace Logclio.Domain.Models
{
    /// <summary>
    /// Represents the search criteria for filtering log entries.
    /// </summary>
    public class LogSearchCriteria
    {
        /// <summary>
        /// Gets or sets the timestamp to search for.
        /// </summary>
        public DateTime? Timestamp { get; set; }

        /// <summary>
        /// Gets or sets the log level to search for.
        /// </summary>
        public string? LogLevel { get; set; }

        /// <summary>
        /// Gets or sets the instance ID to search for.
        /// </summary>
        public string? InstanceId { get; set; }

        /// <summary>
        /// Gets or sets the module to search for.
        /// </summary>
        public string? Module { get; set; }

        /// <summary>
        /// Gets or sets the message to search for.
        /// </summary>
        public string? Message { get; set; }
    }
}
