using Microsoft.Extensions.Configuration;
using Serilog;

namespace Marathon.HangFire
{
    public class GreetingService : IGreetingService
    {
        private readonly ILogger _log;
        private readonly IConfiguration _config;

        public GreetingService(ILogger log, IConfiguration config)
        {
            _log = log;
            _config = config;
        }

        public void Run()
        {
            for (int i = 0; i < _config.GetValue<int>("LoopTimes"); i++)
            {
                _log.Information("The current number is #{runNumber}", i);
            }
        }
    }
}
