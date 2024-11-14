using System;

internal class PeriodicTimer
{
    private TimeSpan timeSpan;

    public PeriodicTimer(TimeSpan timeSpan)
    {
        this.timeSpan = timeSpan;
    }
}