using System;
using System.Collections;
using UnityEngine;

public abstract class BaseItemBehaviour : MonoBehaviour, IItemBehaviour
{
    private bool isPaused;
    private IEnumerator coroutine;
    private ProcessRule processRule;
    private ProcessTimer processTimer = new();

    public event Action<WorkStage> OnProcessStarted;
    public event Action<IItemBehaviour, ProcessRule> OnProcessComplete;

    public abstract ProcessType GetProcessType();
    public abstract WorkStage GetWorkStage();

    public void StartProcess(KitchenItem kitchenItem, ProcessRule rule)
    {
        OnProcessStarted?.Invoke(GetWorkStage());
        processRule = rule;
        processTimer.SetTimer(processRule.processTime);
        coroutine = TickProcess();
        StartCoroutine(coroutine);
    }
    public IEnumerator TickProcess()
    {
        while (!processTimer.IsFinished())
        {
            if (!isPaused) { processTimer.TickTimer(); }
            yield return null;
        }

        FinishProcess();
    }

    public void FinishProcess()
    {
        StopCoroutine(coroutine);
        coroutine = null;
        processTimer.ResetTimer();
        OnProcessComplete?.Invoke(this, processRule);
    }

    public void HandlePauseProcess(KitchenItem kitchenItem)
    {
        isPaused = !isPaused;
        kitchenItem.HandleProcessPauseState(isPaused, GetWorkStage());
    }
}
