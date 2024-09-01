using SSEConsumer.LiveInsects;
using System.Collections.Concurrent;
using System.Collections.Immutable;

namespace SSEConsumer;

public class GameState
{
    public ConcurrentDictionary<Guid, Insect> Insects { get; set; } = [];
    
}
