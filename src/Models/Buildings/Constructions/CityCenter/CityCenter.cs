using Godot;
using System.Collections.Generic;

public class CityCenter : Area2D
{
    public IList<Task<CityCenter>> tasks = new List<Task<CityCenter>>();

    public override void _Ready()
    {
        tasks.Add(new SpawnUnitTask<CityCenter>());
    }

    public override void _Process(float delta)
    {
        if (tasks[0].Execute(this, delta))
        {
            tasks.RemoveAt(0);
        }
    }
}
