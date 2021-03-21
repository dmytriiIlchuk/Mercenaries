using Godot;

public class HitTargetTask<T>: DisplayedTimedTask<T> where T: IAttacking
{
    private readonly IHittable target;

    public HitTargetTask(IHittable target, ProgressBar progressBar, float time) : base(progressBar, time)
    {
        this.target = target;
    }

    public override bool Achieved(T performer)
    {
        return target == null || target.IsDead();
    }

    public override void Action(T performer, float delta)
    {
        performer.Attack(target);
    }
}