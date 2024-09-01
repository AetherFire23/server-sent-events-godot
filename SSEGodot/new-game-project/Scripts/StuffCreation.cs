using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using SSEConsumer.LiveInsects;

namespace NewGameProject.Scripts;

public partial class StuffCreation : Node
{
    private PackedScene _ladyBug;
    private MyRenameTest _sseNode;
    private List<InsectScript> _insectNodes = [];

    public override void _Ready()
    {
        _ladyBug = GD.Load<PackedScene>("res://Player.tscn");
        _sseNode = GetNode<MyRenameTest>("../SSE");
    }

    public override void _Process(double delta)
    {
        var insectGuids = _insectNodes.Select(x => x.Id).ToArray();
        
        var results = Refresher.GetRefreshers(insectGuids, _sseNode.GameState.Insects.Select(x => x.Id));
        
        
        foreach (var appeared in results.appeared)
        {
            var serverInsect = _sseNode.GameState.Insects.First(x => x.Id == appeared);
            var ins = InstantiateBug();
            ins.Id = serverInsect.Id;
            _insectNodes.Add(ins);
        
            ins.Position = new Vector2(serverInsect.X, serverInsect.Y);
        }
        
        foreach (var disappeared in results.disappeared)
        {
            var f = _insectNodes.FirstOrDefault(x => x.Id == disappeared);
        
            if (f is null) return;
            
            f.QueueFree();

            _insectNodes.Remove(f);
        }
        
        foreach (var insectScript in _insectNodes)
        {
            var serverInsect = _sseNode.GameState.Insects.First(x => x.Id == insectScript.Id);
            
            
            // var newPos = insectScript.Position.MoveToward(new Vector2(serverInsect.X, serverInsect.Y), 1);
            // insectScript.Position = newPos;

            insectScript.Position = new Vector2(serverInsect.X, serverInsect.Y);
        }
        
        
    }

    private InsectScript InstantiateBug()
    {
        var instance = _ladyBug.Instantiate<InsectScript>();
        AddChild(instance);

        return instance;
    }

    private void ProcessInsect(Insect serverInsect)
    {
    }
}