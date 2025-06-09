using System.Collections;

public interface ICookableItem
{
    public KitchenItemState CurrentState { get; set; }
    public void StartCook(KitchenItemSO.ProcessRule processRule);
    public IEnumerator CookingProgress(KitchenItemSO.ProcessRule processRule);
    public void OnCookComplete(KitchenItemSO.ProcessRule processRule);
}
