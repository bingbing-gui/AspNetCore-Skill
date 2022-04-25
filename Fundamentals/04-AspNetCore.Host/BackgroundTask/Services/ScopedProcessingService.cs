using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgroundTask.Services
{
    public interface IScopedProcessingService
    {
        Task DoWork(CancellationToken stoppingToken);
    }
    public class ScopedProcessingService : IScopedProcessingService
    {

        private int executionCount = 0;
        private readonly ILogger<ScopedProcessingService> _logger;
        public ScopedProcessingService(ILogger<ScopedProcessingService> logger)
        {
            _logger = logger;
        }
        public async Task DoWork(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                executionCount++;
                _logger.LogInformation("Scoped Processing Service is working. Count: {0} Thread Id={1}", executionCount, Thread.CurrentThread.ManagedThreadId);
                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}
