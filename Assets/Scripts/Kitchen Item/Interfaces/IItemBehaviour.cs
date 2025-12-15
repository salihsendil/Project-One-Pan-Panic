using System;
using System.Collections;

public interface IItemBehaviour
{
    public event Action<IItemBehaviour, ProcessRule> OnProcessComplete;
    public ProcessType GetProcessType();
    public void StartProcess(KitchenItem kitchenItem, ProcessRule rule);
    public IEnumerator TickProcess();
    public void FinishProcess();
    public void SetProcessPause(bool pause);
}
