public class HitTargetTask : Task<Unit>
{
    private Unit target;
    private float cooldown = 1;
    private float status = 0;
    public HitTargetTask(Unit target)
    {
        this.target = target;
    }

    public override bool Achieved(Unit performer)
    {
        return target == null || target.vitality == 0;
    }

    public override void Action(Unit performer, float delta)
    {
        if (status > 0)
        {
            status -= delta;
        }
        else
        {
            status = cooldown;
            target.Hit(performer.attack);
        }
    }
}