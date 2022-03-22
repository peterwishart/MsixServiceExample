using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BackgroundService
{
    public sealed class WindowsBackgroundService : Microsoft.Extensions.Hosting.BackgroundService
    {
        private readonly JokeService _jokeService;
        private readonly ILogger<WindowsBackgroundService> _logger;

        public WindowsBackgroundService(
            JokeService jokeService,
            ILogger<WindowsBackgroundService> logger) =>
            (_jokeService, _logger) = (jokeService, logger);

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogWarning($"WindowsBackgroundService running as PID {Environment.ProcessId} in {(Environment.UserInteractive ? string.Empty : "non-")}interactive environment");
            while (!stoppingToken.IsCancellationRequested)
            {
                string joke = _jokeService.GetJoke();
                _logger.LogWarning(joke);

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }
}