using UnityEngine;

public class ProgressTracker
{
    private float targetValue;
    private float currentValue;

    public bool IsFinished => currentValue >= targetValue /*&& TargetValue > 0*/;
    public float ProgressRatio => targetValue > 0 ? Mathf.Clamp01(currentValue / targetValue) : 0f;

    public void SetTarget(float target)
    {
        targetValue = target;
        currentValue = 0f;
    }

    public void Add(float amount)
    {
        targetValue += amount;
    }

    public void Tick(float deltaTime)
    {
        if (IsFinished) return;

        currentValue += deltaTime;

        if (currentValue > targetValue)
            currentValue = targetValue;
    }

    public void Reset()
    {
        targetValue = currentValue = 0f;
    }
}