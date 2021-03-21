﻿public abstract class TimedTask<T> : Task<T>
{
    protected float currentTime;
    protected float time;

    public TimedTask(float time)
    {
        this.time = time;
        this.currentTime = 0;
    }

    public override bool Execute(T performer, float delta)
    {
        currentTime += delta;
        if (currentTime >= time)
        {
            currentTime = 0;
            return base.Execute(performer, delta);
        }

        return false;
    }
}