namespace Logclio.Domain.Models
{
    /// <summary>
    /// Represents a log file that needs to be processed.
    /// </summary>
    public class LogFile
    {
        /// <summary>
        /// Gets or sets the binary data of the log file.
        /// </summary>
        /// <value>
        /// The binary data of the log file, stored as a byte array.
        /// </value>
        public required byte[] Data { get; set; }

        /// <summary>
        /// Gets or sets the file extension of the log file.
        /// </summary>
        /// <value>
        /// The file extension, which is used to identify the type of the log file (e.g., ".txt", ".csv").
        /// </value>
        public required string Extension { get; set; }
    }
}
