using System.Collections;

public interface ICookableItem
{
    public void StartCook(KitchenItemSO.ProcessRule processRule, IStationTimerDisplayer timerDisplayer);
    public IEnumerator CookingProgress(KitchenItemSO.ProcessRule processRule, IStationTimerDisplayer timerDisplayer);
    public void OnCookComplete(KitchenItemSO.ProcessRule processRule, IStationTimerDisplayer timerDisplayer);
    public void CancelCook(IStationTimerDisplayer timerDisplayer);
}
