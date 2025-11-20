using System;
using System.Collections;
using UnityEngine;

public interface IItemBehaviour
{
    public event Action<IItemBehaviour, ProcessRule> OnProcessComplete;
    public ProcessType GetProcessType();
    public void HandleProcess(KitchenItem item, ProcessRule rule);
    public void StartProcess(KitchenItem kitchenItem, ProcessRule rule);
    public IEnumerator TickProcess();
    public void FinishProcess();
    public void PauseProcess(KitchenItem kitchenItem);
}
