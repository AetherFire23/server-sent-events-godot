using SSEFun.Flushing;
using SSEFun.SSEThings;

namespace SSEFun.Jobs;

public class MyLockstepBackgroundService(Lockstepper lockstepper, SSEManager sseManager)
{
    public async Task Execute()
    {
        // get all users personal data
        var dataForEachuser = await Task.WhenAll(sseManager.UserIds.Select(lockstepper.GetPersonalizedFlushData));

        // send data to all users
        await Task.WhenAll(dataForEachuser.Select(x => sseManager.FlushToUser(x.Item1, x.Item2)));
    }
}
