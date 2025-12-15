using UnityEngine;

public class ProcessTimer
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
        if (Remaining < 2)
        {
            Debug.Log("remaining time in processtimer:" + Remaining);
        }
    }

    public void ResetTimer()
    {
        Duration = Remaining = 0f;
    }
}
