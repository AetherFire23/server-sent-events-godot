using Godot;
using System;

public partial class InsectScript : Node2D
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public int X { get; set; } = 0;
    public int Y { get; set; } = 0;
    
    public int TargetX { get; set; } = 0;
    public int TargetY { get; set; } = 0;
}