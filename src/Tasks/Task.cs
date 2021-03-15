public abstract class Task<T> : IExecutable<T>
{
    public bool Execute(T performer, float delta)
    {
        if (!Achieved(performer))
        {
            Action(performer, delta);
            return false;
        }

        return true;
    }

    public abstract bool Achieved(T performer);

    public abstract void Action(T performer, float delta);
}