
using SSEFun.SSEThings;
using System.Collections.Immutable;

namespace SSEFun.Server;

public class LockService<T>(ISender<T> lockstepper, SSEManagerGeneric sseManager)
{
    public async Task Execute()
    {
        // get all users personal data
        //var dataForEachuser = await Task.WhenAll(sseManager.UserIds.Select(lockstepper.GetPersonalizedFlushData));

        // keep a local variable of clients because we are in multithread context - clients could be added when flushing 
        var cachedClients = sseManager.UserIds.ToImmutableArray();

        var dataForEachuser = await Task.WhenAll(sseManager.UserIds.Select(lockstepper.GetPersonalizedFlushData));

        var clientsAndInfo = cachedClients.Zip(dataForEachuser);

        // send data to all users
        await Task.WhenAll(clientsAndInfo.Select(x => sseManager.FlushToUser(x.First, x.Second)));
    }
}
