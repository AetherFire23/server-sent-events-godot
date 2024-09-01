using System.Collections.Generic;

namespace SSEConsumer.LiveInsects;

public class InsectMoverBackGroundService(GameStateService gameStateService) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // populate Insects

        var insects = Enumerable.Range(0, 1000).Select(x => new Insect() { X = 500, Y = 500 });


        foreach (var item in insects)
        {
            gameStateService.State.Insects.TryAdd(item.Id, item);
        }

        while (!stoppingToken.IsCancellationRequested)
        {

            for (int i = 0; i < 10; i++)
            {
                await Task.Delay(100);
                MoveInsects();
            }

            // delete insect


            var newInsect = new Insect() { X = Random.Shared.Next(0, 700), Y = Random.Shared.Next(0, 700) };

            gameStateService.State.Insects.TryAdd(newInsect.Id, newInsect);

            var ranIndex = Random.Shared.Next(0, gameStateService.State.Insects.Count);
            if (!gameStateService.State.Insects.TryRemove(gameStateService.State.Insects.ElementAt(ranIndex)))
            {
                Console.WriteLine("Failied to delete a bug.");
            }
        }
    }

    private void MoveInsects()
    {
        foreach (var insect in gameStateService.State.Insects)
        {
            insect.Value.X += GetPixelVariation();
            insect.Value.Y += GetPixelVariation();
            //   Console.WriteLine(insect);
        }
    }

    private int GetPixelVariation()
    {
        var direction = Random.Shared.Next(-1, 2);
        var amount = Random.Shared.Next(1, 5);

        var change = direction * amount;
        return change;
    }
}
