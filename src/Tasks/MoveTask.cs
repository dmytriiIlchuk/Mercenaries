using Godot;

public class MoveTask<T> : Task<T> where T : Unit
{
    private Vector2 target;
    private float distance;

    public MoveTask(Vector2 target, float distance = 64.0f)
    {
        this.target = target;
        this.distance = distance;
    }

    public override bool Achieved(T performer)
    {
        return performer.Position.DistanceSquaredTo(target) < distance * distance;
    }

    public override void Action(T performer, float delta)
    {
        performer.Position = performer.Position.MoveToward(target, delta * performer.speed);
    }
}