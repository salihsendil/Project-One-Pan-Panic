using System.Collections;

public interface IItemBehaviour
{
    public ProcessType GetProcessType();
    public void StartProcess(KitchenItem kitchenItem, ProcessRule rule);
    public IEnumerator TickProcess();
    public void CancelProcess();
    public void FinishProcess();
}
