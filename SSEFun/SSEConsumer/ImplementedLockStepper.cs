using SSEConsumer.LiveInsects;
using SSEFun.Server;
using SSFun.Shared;

namespace SSEConsumer;

public class ImplementedLockStepper(GameStateService gameStateService) : ISender<GameStateClient>
{
    public async Task<GameStateClient> GetPersonalizedFlushData(Guid userId)
    {

        var gClient = new GameStateClient()
        {
            Insects = gameStateService.State.Insects.Values.ToList(),
        };

        return gClient;
    }
}
