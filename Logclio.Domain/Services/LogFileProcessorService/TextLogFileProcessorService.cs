using Logclio.Domain.Models;
using Logclio.Domain.Repositories.LogEntry;
using Microsoft.Extensions.Logging;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Logclio.Domain.Services.LogFileProcessorService
{
    public class TextLogFileProcessorService(
        ILogEntryRepository logEntryRepository,
        ILogger<TextLogFileProcessorService> logger) : ILogFileProcessorService
    {
        public async Task ProcessLogFile(LogFile logFile)
        {
            var tempFilePath = Path.GetTempFileName() + logFile.Extension;
            File.WriteAllBytes(tempFilePath, logFile.Data);
            var logEntries = await ParseLogFile(tempFilePath);
            foreach (var logEntry in logEntries)
            {
                logEntryRepository.AddLogEntry(logEntry);
            }

            File.Delete(tempFilePath);
        }

        private async Task<IEnumerable<LogEntry>> ParseLogFile(string filePath)
        {
            var logEntries = new List<LogEntry>();
            var lines = await File.ReadAllLinesAsync(filePath);
            var linesCount = 0;

            foreach (var line in lines)
            {
                var logEntry = ParseLogLine(line);
                if (logEntry != null)
                {
                    logEntries.Add(logEntry);
                }
                linesCount++;
            }

            logger.LogInformation($"Successfully parsed through {linesCount} lines of file");

            return logEntries;
        }

        private LogEntry ParseLogLine(string line)
        {
            var regex = new Regex(@"^(?<timestamp>\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2}\.\d{3} \+\d{2}:\d{2}) \[(?<logLevel>[A-Z]+)\] \{(?<instanceId>[^\}]+)\} \[(?<module>[^\]]+)\] (?<message>.+)$");
            var match = regex.Match(line);
            if (!match.Success) return null;

            return new LogEntry
            {
                Timestamp = DateTime.ParseExact(match.Groups["timestamp"].Value, "yyyy-MM-dd HH:mm:ss.fff zzz", CultureInfo.InvariantCulture),
                LogLevel = match.Groups["logLevel"].Value,
                InstanceId = match.Groups["instanceId"].Value,
                Module = match.Groups["module"].Value,
                Message = match.Groups["message"].Value
            };
        }
    }
}
