using Godot;

public class ObjectiveProvider
{
    public static IExecutable<Unit> AttackTargetObjective(Unit target, ProgressBar progressBar, float time) => new Objective<Unit>(new MoveTask<Unit>(target.Position), new HitTargetTask(target, progressBar, time));

    public static IExecutable<Unit> MoveToTargetObjective(Unit target)
    {
        return new MoveTask<Unit>(target.Position);
    }
}