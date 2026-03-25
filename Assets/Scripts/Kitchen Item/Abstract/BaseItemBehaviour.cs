using System;
using UnityEngine;

[RequireComponent(typeof(ItemBehaviourController))]
public abstract class BaseItemBehaviour : MonoBehaviour, IItemBehaviour
{
    private ProgressTracker progressTracker;

    public event Action OnProcessComplete;
    
    public abstract ProcessType GetProcessType();

    public void StartProcess(ProcessRule rule)
    {
        progressTracker.SetTarget(rule.ProcessTime);
    }

    public void TickProcess(float delta)
    {
        Debug.Log("tick " + delta);

        progressTracker.Tick(delta);

        if (progressTracker.IsFinished)
        {
            FinishProcess();
        }
    }

    public void FinishProcess()
    {
        OnProcessComplete?.Invoke();
    }
}
