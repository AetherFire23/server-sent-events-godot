using SSEFun.Flushing;
using SSEFun.Jobs;
using SSEFun.SSEThings;
using SSEFun.TimeHostedService;

namespace SSEFun.Initialization;

public static class ServiceRegistrar
{
    public static void RegisterServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<SSEManager>();
        serviceCollection.AddSingleton<Lockstepper>();

        serviceCollection.AddSingleton<MyLockstepBackgroundService>();
        serviceCollection.AddHostedService<MyTimeHostedService>();
    }
}
