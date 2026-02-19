public class Timer
{
    public float Duration;
    public float Remaining;

    public bool IsFinished() => Remaining <= 0f;

    public void Set(float duration)
    {
        Reset();
        Duration = duration;
        Remaining = duration;
    }

    public void Tick(float deltaTime)
    {
        Remaining -= deltaTime;
    }

    public void Reset()
    {
        Duration = Remaining = 0f;
    }
}
