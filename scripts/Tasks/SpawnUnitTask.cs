using Godot;

class SpawnUnitTask<T> : Task<T> where T : Node2D
{
    float timePeriod = 3;
    float time = 0;
    public override bool Execute(T performer, float delta)
    {
        time += delta;
        if (time > timePeriod)
        {
            performer.GetParent().AddChild(GameObjectFactory.MakeUnit(performer.Position));
            time = 0;
        }

        return false;
    }
}
