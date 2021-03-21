using Godot;

public class MoveToPointTask<T> : Task<T> where T : Node2D, IMoving
{
    protected Vector2 target;
    private float distance;

    public MoveToPointTask(Vector2 target, float distance = 64.0f)
    {
        this.target = target;
        this.distance = distance;
    }

    public override bool Achieved(T performer)
    {
        return performer.At(target, distance);
    }

    public override void Action(T performer, float delta)
    {
        performer.MoveTo(target, delta);
    }
}