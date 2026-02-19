using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ItemBehaviourController))]
public abstract class BaseItemBehaviour : MonoBehaviour, IItemBehaviour
{
    private bool isPaused;
    private IEnumerator coroutine;
    private ProcessRule processRule;
    private Timer processTimer = new();

    public event Action<IItemBehaviour, ProcessRule> OnProcessComplete;

    public abstract ProcessType GetProcessType();

    public void StartProcess(ProcessRule rule)
    {
        processRule = rule;
        processTimer.Set(processRule.ProcessTime);
        coroutine = TickProcess();
        StartCoroutine(coroutine);
    }

    public IEnumerator TickProcess()
    {
        while (!processTimer.IsFinished())
        {
            if (!isPaused) { processTimer.Tick(Time.deltaTime); }
            yield return null;
        }

        FinishProcess();
    }

    public void FinishProcess()
    {
        StopCoroutine(coroutine);
        coroutine = null;
        processTimer.Reset();
        OnProcessComplete?.Invoke(this, processRule);
    }

    public void SetProcessPause(bool pause)
    {
        isPaused = pause;
    }
}
