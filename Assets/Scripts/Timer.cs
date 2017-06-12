using UnityEngine;

class Timer
{

    private long lastTime;

    public Timer(int timeDelta = 1000, bool useFixedTime = true)
    {
        TimeDelta = timeDelta;
        UseFixedTime = useFixedTime;
    }

    public int TimeDelta { get; set; }
    public bool UseFixedTime { get; set; }

    public bool IsTimeUp()
    {
        var millis = getCurrentMillis();
        if ((millis % TimeDelta == 0) && millis != lastTime)
        {
            lastTime = millis;
            return true;
        }

        return false;
    }



    private long getCurrentMillis()
    {
        var time = UseFixedTime ? Time.fixedTime : Time.time;
        return (long)time * 1000;
    }
}
