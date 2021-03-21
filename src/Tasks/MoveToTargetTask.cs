using Godot;

public class MoveToTargetTask<T>: MoveToPointTask<T> where T: Node2D, IMoving
{
    private Node2D targetNode;
    public MoveToTargetTask(Node2D target, float distance = 64) : base(target.Position, distance)
    {
        this.targetNode = target;
    }

    public override bool Achieved(T performer)
    {
        this.target = targetNode.Position;
        return base.Achieved(performer);
    }

    public override void Action(T performer, float delta)
    {
        this.target = targetNode.Position;
        base.Action(performer, delta);
    }
}