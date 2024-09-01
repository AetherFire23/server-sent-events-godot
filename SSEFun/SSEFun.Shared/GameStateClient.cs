using SSEConsumer.LiveInsects;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSFun.Shared;

public class GameStateClient
{
    public List<Insect> Insects { get; set; } = [];
}
