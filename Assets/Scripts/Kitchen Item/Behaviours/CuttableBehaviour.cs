using System;
using System.Collections;
using UnityEngine;

public class CuttableBehaviour : MonoBehaviour, IItemBehaviour
{
    private bool isPaused;
    private bool isProcessing;
    private IEnumerator coroutine;
    private ProcessRule processRule;
    private ProcessTimer processTimer = new();

    public event Action<IItemBehaviour, ProcessRule> OnProcessComplete;

    public ProcessType GetProcessType() => ProcessType.Cut;

    public void HandleProcess(KitchenItem item, ProcessRule rule)
    {
        if (!isProcessing)
        {
            StartProcess(item, rule);
            return;
        }

        PauseProcess(item);
    }

    public void StartProcess(KitchenItem kitchenItem, ProcessRule rule)
    {
        processRule = rule;
        kitchenItem.WorkStage = WorkStage.Cutting;
        processTimer.SetTimer(processRule.processTime);
        isProcessing = true;
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
        isProcessing = false;
        processTimer.ResetTimer();
        StopCoroutine(coroutine);
        coroutine = null;
        OnProcessComplete?.Invoke(this, processRule);
    }

    public void PauseProcess(KitchenItem kitchenItem)
    {
        isPaused = !isPaused;

        kitchenItem.WorkStage = isPaused ? WorkStage.Idle : WorkStage.Cutting;
    }
}
