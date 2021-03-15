public class ObjectiveProvider
{
    public static IExecutable<Unit> AttackTargetObjective(Unit target) => new Objective<Unit>(new MoveTask<Unit>(target.Position), new HitTargetTask(target));

    public static IExecutable<Unit> MoveToTargetObjective(Unit target)
    {
        return new MoveTask<Unit>(target.Position);
    }
}