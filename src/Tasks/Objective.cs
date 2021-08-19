using System.Collections.Generic;
using System.Linq;

public class Objective<T> : Task<T>
{
    protected IList<Task<T>> tasks;

    public Objective(params Task<T>[] tasks)
    {
        this.tasks = tasks.ToList();
    }

    public override bool Achieved(T performer)
    {
        return tasks.Last().Achieved(performer);
    }

    public override void Action(T performer, float delta)
    {
        Task<T> currentTask = tasks.FirstOrDefault(task => !task.Achieved(performer));

        currentTask?.Action(performer, delta);
    }
}