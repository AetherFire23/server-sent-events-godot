using SSEFun.Jobs;

namespace SSEFun.TimeHostedService;

public class MyTimeHostedService(MyLockstepBackgroundService lockstepService) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await lockstepService.Execute();
            await Task.Delay(100, stoppingToken);
        }
    }
}
