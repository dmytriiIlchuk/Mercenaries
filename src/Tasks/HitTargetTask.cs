using Godot;

public class HitTargetTask : DisplayedTimedTask<Unit>
{
    private Unit target;

    public HitTargetTask(Unit target, ProgressBar progressBar, float time) : base(progressBar, time)
    {
        this.target = target;
    }

    public override bool Achieved(Unit performer)
    {
        return target == null || target.vitality == 0;
    }

    public override void Action(Unit performer, float delta)
    {
        target.Hit(performer.attack);
    }
}