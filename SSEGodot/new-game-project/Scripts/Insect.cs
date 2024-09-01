using System;

namespace SSEConsumer.LiveInsects;

public class Insect : IPosition
{
    public Guid Id { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
} 
