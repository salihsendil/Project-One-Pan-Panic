using System.Collections;

public interface ICuttableItem 
{
    public void StartCut(KitchenItemSO.ProcessRule processRule, ITransferItemHandler player);
    public IEnumerator CuttingProgress(KitchenItemSO.ProcessRule processRule, ITransferItemHandler player);
    public void OnCutComplete(KitchenItemSO.ProcessRule processRule, ITransferItemHandler player);
}
