using System;
using UnityEngine;

public abstract class BaseItemProcess : MonoBehaviour, IItemProcess
{
    //Private Variables
    [SerializeField] private ProcessStage processStage;
    private ProgressTracker progressTracker = new();

    //Protected Variables
    [SerializeField] protected ProcessRuleSO processRule;
    protected IProcessDisplayer processDisplayer;

    //Update Display Optimization
    private float timerTickDelayCounter = 0;

    //Properties
    public ProcessStage ProcessStage => processStage;
    public ProcessRuleSO ProcessRule => processRule;

    //Events
    public event Action<IItemProcess> OnProcessFinished;

    private void OnDisable()
    {
        timerTickDelayCounter = 0;
        processStage = ProcessStage.None;
        progressTracker.Reset();
        processDisplayer.Disable();
    }

    public bool CanProcess(ProcessType processType, ItemStage stage)
    {
        return processRule != null && processRule.ProcessType == processType && stage == processRule.FromStage;
    }

    public virtual void StartProcess()
    {
        if (processStage == ProcessStage.None)
        {
            timerTickDelayCounter = 0;
            progressTracker.SetTarget(processRule.ProcessTime);
            processDisplayer.Initialize();
        }

        processStage = ProcessStage.Processing;
    }

    public void TickProcess(float deltaTime)
    {
        if (processStage != ProcessStage.Processing) return;

        progressTracker.Tick(deltaTime);

        timerTickDelayCounter += deltaTime;
        if (timerTickDelayCounter >= 0.1f)
        {
            timerTickDelayCounter = 0;
            processDisplayer.Tick(progressTracker.ProgressRatio);
        }

        if (progressTracker.IsFinished)
        {
            FinishProcess();
        }
    }

    public virtual void PauseProcess()
    {
        if (processStage == ProcessStage.Processing)
        {
            processStage = ProcessStage.Pause;
        }
    }

    public virtual void FinishProcess()
    {
        processStage = ProcessStage.None;
        progressTracker.Reset();
        processDisplayer.Disable();
        OnProcessFinished?.Invoke(this);
    }
}
