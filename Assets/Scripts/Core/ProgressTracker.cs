using UnityEngine;

public class ProgressTracker
{
    public float TargetValue { get; private set; }
    public float CurrentValue { get; private set; }

    public bool IsFinished => CurrentValue >= TargetValue /*&& TargetValue > 0*/;
    public float ProgressRatio => TargetValue > 0 ? Mathf.Clamp01(CurrentValue / TargetValue) : 0f;

    public void SetTarget(float target)
    {
        TargetValue = target;
        CurrentValue = 0f;
    }

    public void Tick(float deltaTime)
    {
        if (IsFinished) return;

        CurrentValue += deltaTime;

        if (CurrentValue > TargetValue)
            CurrentValue = TargetValue;
    }

    public void Reset()
    {
        TargetValue = CurrentValue = 0f;
    }
}