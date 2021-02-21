public abstract class Task<T>
{
    public abstract bool Execute(T performer, float delta);
}
