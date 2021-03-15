using System.Collections.Generic;
using System.Linq;

public class Objective<T> : IExecutable<T>
{
    protected IList<Task<T>> tasks;

    public Objective(params Task<T>[] tasks)
    {
        this.tasks = tasks.ToList();
    }

    public bool Execute(T performer, float delta)
    {
        Task<T> currentTask = tasks.FirstOrDefault(task => !task.Achieved(performer));
        return currentTask == null || currentTask.Execute(performer, delta);
    }
}