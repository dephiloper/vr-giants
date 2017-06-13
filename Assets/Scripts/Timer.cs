using UnityEngine;

class Timer
{

    private long startTime;

    public Timer(int timeDelta = 1000, bool useFixedTime = true)
    {
        TimeDelta = timeDelta;
        UseFixedTime = useFixedTime;
        Reset();
    }

    public int TimeDelta { get; set; }
    public bool UseFixedTime { get; set; }

    public bool IsTimeUp()
    {
        var millis = GetCurrentMillis() - startTime;
        return millis > TimeDelta;
    }

    public void Reset()
    {
        startTime = GetCurrentMillis();
    }

    private long GetCurrentMillis()
    {
        var time = UseFixedTime ? Time.fixedTime : Time.time;
        return (long)time * 1000;
    }
}
