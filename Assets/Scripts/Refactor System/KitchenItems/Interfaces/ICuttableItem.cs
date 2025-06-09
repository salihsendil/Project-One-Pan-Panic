using System.Collections;

public interface ICuttableItem 
{
    public KitchenItemState CurrentState { get; set; }
    public void StartCut(KitchenItemSO.ProcessRule processRule);
    public IEnumerator CuttingProgress(KitchenItemSO.ProcessRule processRule);
    public void OnCutComplete(KitchenItemSO.ProcessRule processRule);
}
