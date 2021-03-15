using Godot;

public class MoveTask<T> : Task<T> where T : Unit
{
    private Vector2 target;

    public MoveTask(Vector2 target)
    {
        this.target = target;
    }

    public override bool Achieved(T performer)
    {
        return performer.Position.IsEqualApprox(target);
    }

    public override void Action(T performer, float delta)
    {
        performer.Position = performer.Position.MoveToward(target, delta * performer.speed);
    }
}