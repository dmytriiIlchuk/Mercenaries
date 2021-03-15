public interface IExecutable<T>
{
    bool Execute(T performer, float delta);
}