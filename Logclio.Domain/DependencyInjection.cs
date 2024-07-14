using Logclio.Domain.BackgroundServices;
using Logclio.Domain.MemoryStorage;
using Logclio.Domain.Repositories.LogEntry;
using Logclio.Domain.Services.LogFileProcessorService;
using Logclio.Domain.Services.LogFileQueueService;
using Logclio.Domain.Services.LogProcessorFactory;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Logclio.Domain
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Configures log processing services.
        /// </summary>
        /// <param name="builder">The WebApplicationBuilder to add services to.</param>
        /// <returns>The WebApplicationBuilder with services configured.</returns>
        public static WebApplicationBuilder ConfigureLogProcessingServices(this WebApplicationBuilder builder)
        {
            //Storage
            builder.Services.AddSingleton<ILogEntriesStorage, LogEntriesStorage>();
            builder.Services.AddSingleton<ILogFileQueueService, LogFileQueueService>();

            //Repositores
            builder.Services.AddTransient<ILogEntryRepository, LogEntryRepository>();

            //Processing
            builder.Services.AddScoped<TextLogFileProcessorService>();
            builder.Services.AddSingleton<ILogProcessorFactory, LogProcessorFactory>();
            builder.Services.AddHostedService<LogFileBackgroundProcessorService>();

            return builder;
        }
    }
}
