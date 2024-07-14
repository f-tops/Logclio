using Asp.Versioning;
using Logclio.Domain.Common.Constants;
using Logclio.Domain.Models;
using Logclio.Domain.Repositories.LogEntry;
using Logclio.Domain.Services.LogFileQueueService;
using Microsoft.AspNetCore.Mvc;

namespace Logclio.Api.Controllers
{
    /// <summary>
    /// Controller for managing log files and log entries.
    /// </summary>
    [Route("api/v{version:apiVersion}/logs")]
    [ApiVersion("1")]
    [ApiController]
    public class LogsController(
        ILogFileQueueService logFileQueueService,
        ILogEntryRepository logEntryRepository) : ControllerBase
    {
        /// <summary>
        /// Uploads a log file for processing.
        /// </summary>
        /// <param name="file">The log file to upload.</param>
        /// <returns>A status indicating the result of the upload operation.</returns>
        [HttpPost("upload")]
        public async Task<IActionResult> UploadAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                var fileExtension = Path.GetExtension(file.FileName);
                if (!fileExtension.Equals(FileExtensionConstants.TXT_EXTENSION, StringComparison.OrdinalIgnoreCase))
                {
                    return BadRequest("Unsupported file extension!");
                }

                var logFile = new LogFile
                {
                    Data = memoryStream.ToArray(),
                    Extension = fileExtension
                };

                logFileQueueService.EnqueueLogFile(logFile);
            }

            return Ok("File uploaded successfully.");
        }

        /// <summary>
        /// Searches log entries based on the specified criteria.
        /// </summary>
        /// <param name="criteria">The search criteria for filtering log entries.</param>
        /// <returns>A collection of log entries that match the search criteria.</returns>
        [HttpPost("search")]
        public async Task<ActionResult<IEnumerable<LogEntry>>> SearchAsync([FromBody] LogSearchCriteria criteria)
        {
            var results = logEntryRepository.Search(criteria);
            return Ok(results);
        }
    }
}
