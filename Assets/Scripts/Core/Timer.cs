
using System;

[Serializable]
public class Timer
{
    public float Duration;
    public float Remaining;

    public bool IsFinished => Remaining <= 0;
    public float RemainingRatio => Remaining / Duration;

    public void Set(float duration)
    {
        Remaining = Duration = duration;
    }

    public void Tick(float deltaTime)
    {
        Remaining -= deltaTime;

        if (Remaining < 0)
            Remaining = 0;
    }

    public void Reset()
    {
        Duration = Remaining = 0f;
    }
}