using System;

public interface IItemBehaviour
{
    public event Action OnProcessComplete;
    public ProcessType GetProcessType();
    public void StartProcess(ProcessRule rule);
    public void TickProcess(float delta);
    public void FinishProcess();
}
