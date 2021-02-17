using Godot;

public class MoveTask<T> : Task<T> where T : Unit
{
    private Vector2 target;
    public MoveTask(Vector2 target)
    {
        this.target = target;
    }
    public override bool Execute(T performer, float delta)
    {
        performer.Position = performer.Position.MoveToward(target, delta * performer.speed);
        if (performer.Position.IsEqualApprox(target))
        {
            return true;
        }

        return false;
    }
}
