using Godot;
using System.Collections.Generic;

public class Unit : Node2D
{
    // Declare member variables here. Examples:
    public int speed = 10;
    public IList<Task<Unit>> tasks = new List<Task<Unit>>();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if (tasks[0].Execute(this, delta))
        {
            tasks.RemoveAt(0);
        }
    }

    public Task<Unit> GetNextTask()
    {
        return (tasks.Count > 0) ? tasks[0] : null;
    }
}
