using UnityEngine;

/// <summary>
/// Represents a generic all purpose timer. It allowes you to check if a certain amount of time has passed since the 
/// start or the last time it has been reseted.
/// </summary>
public class Timer
{
    private long startTime;

    /// <summary>
    /// Initializes a new instance of the <see cref="Timer"/> class and also starts the timer right away. 
    /// </summary>
    /// <param name="timeDelta">Sets the amout of millis the <see cref="Timer"/> has to wait. Defaults to 1000 ms.</param>
    /// <param name="useFixedTime">Sets if the <see cref="Timer"/> should use <see cref="Time.fixedTime" /> instead of <see cref="Time.time"/>. Defaults to true</param>
    public Timer(int timeDelta = 1000, bool useFixedTime = true){
        TimeDelta = timeDelta;
        UseFixedTime = useFixedTime;
        Reset();
    }

    /// <summary>
    /// Gets or sets the amout of millis the <see cref="Timer"/> has to wait.
    /// </summary>
    public int TimeDelta { get; set; }

    /// <summary>
    /// Gets or sets if the <see cref="Timer"/> should use <see cref="Time.fixedTime" /> instead of <see cref="Time.time"/>.
    /// </summary>
    public bool UseFixedTime { get; set; }

    /// <summary>
    /// Checks if <see cref="TimeDelta"/> millis have passed.
    /// </summary>
    /// <returns>Returns true if the time is up.</returns>
    public bool IsTimeUp(){
        var millis = GetCurrentMillis() - startTime;
        return millis > TimeDelta;
    }

    /// <summary>
    /// Resets the current <see cref="Timer"/> instance.
    /// </summary>
    public void Reset(){
        startTime = GetCurrentMillis();
    }

    private long GetCurrentMillis(){
        var time = UseFixedTime ? Time.fixedTime : Time.time;
        return (long) time * 1000;
    }
}