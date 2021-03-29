using Godot;

public class HitTargetTask<T>: TimedTask<T> where T: IAttacking
{
    private readonly IHittable target;

    public HitTargetTask(IHittable target, float time) : base(time)
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