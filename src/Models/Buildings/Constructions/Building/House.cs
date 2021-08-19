using System.Collections.Generic;

public class House : Building
{
    public IList<Task<House>> tasks = new List<Task<House>>();
    public int capacity = 4;

    public override void _Ready()
    {
        //tasks.Add(new SpawnGameObjectTask<House>(3, GameObjectFactory.MakeObject(UnitConfig.Default, this, Position), 4));
    }

    public override void _Process(float delta)
    {
        if (tasks.Count > 0 && tasks[0].Execute(this, delta))
        {
            tasks.RemoveAt(0);
        }
    }
}
