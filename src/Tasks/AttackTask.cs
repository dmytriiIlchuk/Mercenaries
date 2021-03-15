using Godot;

public class AttackTask : Task<Unit>
{
    private Unit target;

    public AttackTask(Unit target)
    {
        this.target = target;
    }

    public override bool Achieved(Unit performer)
    {
        return target == null || target.IsQueuedForDeletion();
    }

    public override void Action(Unit performer, float delta)
    {
        target.Hit();
    }
}