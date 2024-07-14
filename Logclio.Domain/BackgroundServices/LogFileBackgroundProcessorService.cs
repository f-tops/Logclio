using Logclio.Domain.Services.LogFileQueueService;
using Logclio.Domain.Services.LogProcessorFactory;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Logclio.Domain.BackgroundServices
{
    /// <summary>
    /// Background service responsible for processing enqueued log files from the queue service.
    /// </summary>
    public class LogFileBackgroundProcessorService(
        ILogFileQueueService logFileQueueService,
        ILogProcessorFactory logProcessorFactory,
        ILogger<LogFileBackgroundProcessorService> logger) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            await Task.Delay(2000, cancellationToken);
            logger.LogInformation("Starting log file processor background service");

            while (!cancellationToken.IsCancellationRequested)
            {
                var logFile = logFileQueueService.DequeueLogFile();
                if (logFile != null)
                {
                    logger.LogInformation("Starting to process enqueued log file..");
                    var logProcessorService = logProcessorFactory.GetProcessor(logFile.Extension);

                    try
                    {
                        await logProcessorService.ProcessLogFile(logFile);
                        logger.LogInformation("Sucessfully processed log file..");
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "Something went wrong while trying to process log file.");
                    }
                }
            }
        }
    }
}
