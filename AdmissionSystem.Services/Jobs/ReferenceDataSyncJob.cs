
using Quartz;
using Microsoft.Extensions.Logging;

namespace AdmissionSystem.Services.Jobs
{
    public class ReferenceDataSyncJob : IJob
    {
        private readonly ILogger<ReferenceDataSyncJob> _logger;

        public ReferenceDataSyncJob(ILogger<ReferenceDataSyncJob> logger)
        {
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Reference Data Sync Job started at: {Time}", DateTime.Now);
            
          
            
            _logger.LogInformation("Reference Data Sync Job completed at: {Time}", DateTime.Now);
            await Task.CompletedTask;
        }
    }
}