using Godot;
public abstract class DisplayedTimedTask<T> : TimedTask<T>
{
    private ProgressBar progressBar;

    public DisplayedTimedTask(ProgressBar progressBar, float time): base(time)
    {
        this.progressBar = progressBar;
        progressBar.MaxValue = time;
        progressBar.Value = currentTime;
    }

    public override bool Execute(T performer, float delta)
    {
        bool result = base.Execute(performer, delta);
        progressBar.Value = currentTime;

        return result;
    }
}