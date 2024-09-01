
using Microsoft.Extensions.DependencyInjection;
using SSEFun.SSEThings;

namespace SSEFun.Server;

public static class ServiceRegisterSSE
{
    public static void RegisterSSEServices<TJob, TState>(this IServiceCollection serviceCollection,
        Action<SSEOptions> optionsSelector)
        where TJob : class, ISender<TState>
        where TState : class
    {
        serviceCollection.AddSingleton<SSEManagerGeneric>();
        serviceCollection.AddSingleton<LockService<TState>>();

        serviceCollection.AddSingleton<ISender<TState>, TJob>();

        serviceCollection.AddHostedService(x =>
        {
            var backgroundService = x.GetRequiredService<LockService<TState>>();

            var sseOptions = new SSEOptions();
            optionsSelector(sseOptions);

            var initializedService = new MyTimeHostedService<TState>(backgroundService, sseOptions);

            return initializedService;
        });
    }
}

public class SSEOptions
{
    public float IntervalInMilliseconds { get; set; } = 100;
}