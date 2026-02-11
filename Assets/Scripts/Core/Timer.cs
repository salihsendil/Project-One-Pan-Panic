using UnityEngine;

public class Timer
{
    public float Duration;
    public float Remaining;

    public bool IsFinished() => Remaining <= 0f;

    public void SetTimer(float duration)
    {
        ResetTimer();
        Duration = duration;
        Remaining = duration;
    }

    public void TickTimer()
    {
        Remaining -= Time.deltaTime;
    }

    public void ResetTimer()
    {
        Duration = Remaining = 0f;
    }
}
