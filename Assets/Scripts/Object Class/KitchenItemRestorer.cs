using Zenject;

public class KitchenItemRestorer
{
    [Inject] private KitchenItemPoolManager poolManager;
    [Inject] private ContainerDispenserSystem containerDispenser;

    public void RestoreKitchenItemByType(KitchenItem item)
    {
        if (item.TryGetComponent(out IContainerItem containerItem))
        {
            for (int i = 0; i < containerItem.OnContainerList.Count; i++)
            {
                RestoreKitchenItem(containerItem.OnContainerList[i]);
                poolManager.ReturnKitchenItemToPool(containerItem.OnContainerList[i]);
            }

            RestoreKitchenItem(item);
            containerItem.OnContainerList.Clear(); //check profiller for gc
            containerDispenser.ReturnPlateToStack(item);

        }

        else
        {
            RestoreKitchenItem(item);
            poolManager.ReturnKitchenItemToPool(item);
        }

    }



    public void RestoreKitchenItem(KitchenItem item)
    {
        if (item.TryGetComponent(out IKitchenItemStateProvider stateProvider))
        {
            item.UpdateVisual(item.KitchenItemData.baseMesh);
            item.IsProcessed = false;
            item.gameObject.transform.SetParent(null);
            stateProvider.CurrentState = item.KitchenItemData.baseState;
        }
    }
}
