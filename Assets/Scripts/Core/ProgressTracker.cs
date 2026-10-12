using System;
using UnityEngine;


// This class is kind of a reversed timer. It can track a process.
// It has Set, Tick, and Reset methods.


[Serializable]
public class ProgressTracker
{
    [SerializeField] private float current = 0f;
    [SerializeField] private float target = 0f;


    public float Current => current;
    public float Target => target;


    public bool IsFinished => current >= target && target > 0;
    public float ProgressRatio => target > 0 ? Mathf.Clamp01(current / target) : 0f;


    public void Set(float duration)
    {
        current = 0;
        target = duration;
    }

    public void Set(float progress, float duration)
    {
        current = progress;
        target = duration;
    }

    public void Tick(float deltaTime)
    {
        if (IsFinished)
            return;

        current += deltaTime;
        Debug.Log("current time: " + current);

        if (current >= target)
            current = target;
    }

    public void Reset()
    {
        current = target = 0f;
    }
}
