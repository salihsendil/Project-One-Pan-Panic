using System;
using System.Collections;

public interface IItemBehaviour
{
    public event Action<WorkStage> OnProcessStarted;

    public event Action<IItemBehaviour, ProcessRule> OnProcessComplete;
    public WorkStage GetWorkStage();
    public ProcessType GetProcessType();
    public void StartProcess(KitchenItem kitchenItem, ProcessRule rule);
    public IEnumerator TickProcess();
    public void FinishProcess();
    public void HandlePauseState(KitchenItem kitchenItem);
}
