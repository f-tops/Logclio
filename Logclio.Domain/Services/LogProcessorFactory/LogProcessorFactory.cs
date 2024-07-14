using Logclio.Domain.Common.Constants;
using Logclio.Domain.Services.LogFileProcessorService;
using Microsoft.Extensions.DependencyInjection;

namespace Logclio.Domain.Services.LogProcessorFactory
{
    public class LogProcessorFactory : ILogProcessorFactory
    {
        private readonly Dictionary<string, ILogFileProcessorService> _processors;

        public LogProcessorFactory(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var logFileProcessorService = scope.ServiceProvider.GetRequiredService<TextLogFileProcessorService>();
            _processors = new Dictionary<string, ILogFileProcessorService>
            {
                { FileExtensionConstants.TXT_EXTENSION, logFileProcessorService }
            };
        }

        public ILogFileProcessorService GetProcessor(string extension)
        {
            if (_processors.TryGetValue(extension, out var processor))
            {
                return processor;
            }

            throw new NotSupportedException($"File extension {extension} is not supported.");
        }
    }
}
