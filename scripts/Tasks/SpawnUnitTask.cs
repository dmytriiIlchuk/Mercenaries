using Godot;

class SpawnUnitTask<T> : Task<T> where T : Node2D
{
    float timePeriod = 3;
    float time = 0;
    int amount = 3;
    public override bool Execute(T performer, float delta)
    {
        time += delta;
        if (time > timePeriod && amount > 0)
        {
            performer.GetParent().AddChild(GameObjectFactory.MakeUnit(performer.Position));
            time = 0;
            amount--;
        }

        return false;
    }
}
