using System;
using System.Collections;

public interface ICuttableItem 
{
    public void StartCut(KitchenItemSO.ProcessRule processRule, ITransferItemHandler player, IStationTimerDisplayer timerDisplayer);
    public IEnumerator CuttingProgress(KitchenItemSO.ProcessRule processRule, ITransferItemHandler player, IStationTimerDisplayer timerDisplayer);
    public void OnCutComplete(KitchenItemSO.ProcessRule processRule, ITransferItemHandler player, IStationTimerDisplayer timerDisplayer);
}
