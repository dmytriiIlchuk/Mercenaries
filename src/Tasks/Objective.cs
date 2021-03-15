using System.Collections.Generic;
using System.Linq;

public class Objective<T> : IExecutable<T>
{
    IList<Task<T>> tasks;

    public Objective()
    {
    }

    public bool Execute(T performer, float delta)
    {
        return tasks.First(task => !task.Achieved(performer)).Execute(performer, delta);
    }
}