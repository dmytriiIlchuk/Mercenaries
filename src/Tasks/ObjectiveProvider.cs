using Godot;

public class ObjectiveProvider
{
    public static IExecutable<T> AttackTargetObjective<T, T1>(T1 target, ProgressBar progressBar, float time) where T: Node2D, IMoving, IAttacking where T1: Node2D, IHittable => new Objective<T>(new MoveToTargetTask<T>(target), new HitTargetTask<T>(target, progressBar, time));

    public static IExecutable<T> MoveToTargetObjective<T>(Node2D target) where T: Node2D, IMoving
    {
        return new MoveToPointTask<T>(target.Position);
    }
}