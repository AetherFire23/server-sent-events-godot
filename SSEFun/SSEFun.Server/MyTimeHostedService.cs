using Microsoft.Extensions.Hosting;
using SSEFun.Server;

public class MyTimeHostedService<T> : BackgroundService
{
    private readonly LockService<T> _sender;
    private readonly SSEOptions _options;

    public MyTimeHostedService(LockService<T> sender, SSEOptions options)
    {
        _sender = sender;
        _options = options;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _sender.Execute();
                await Task.Delay(TimeSpan.FromMilliseconds(_options.IntervalInMilliseconds), stoppingToken);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}