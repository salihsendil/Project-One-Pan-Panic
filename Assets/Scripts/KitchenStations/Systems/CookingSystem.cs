using UnityEngine;

public class CookingSystem : KitchenStation
{
    [SerializeField] private IStationTimerDisplayer timerDisplayer;

    private void Awake()
    {
        TryGetComponent(out timerDisplayer);
    }

    public override void Interact()
    {
        if (transferItemHandler == null) { return; }

        if (!IsOccupied && transferItemHandler.HasKitchenItem)
        {
            if (transferItemHandler.GetKitchenItem.TryGetComponent<ICookableItem>(out var cookableItem))
            {
                transferItemHandler.GiveKitchenItem(out var kitchenItem);
                PlaceKitchenItem(kitchenItem);

                currentKitchenItem.TryGetComponent<IKitchenItemStateProvider>(out IKitchenItemStateProvider stateProvider);

                if (currentKitchenItem.KitchenItemData.GetProcessRuleMatch(stateProvider.CurrentState, out KitchenItemSO.ProcessRule rule))
                {
                    timerDisplayer.ShowTimer();
                    cookableItem.StartCook(rule, timerDisplayer);
                }
            }

            else
            {
                //ui warning pop up
                Debug.Log("Bu eþya piþirilemez!");
            }

        }

        else if (IsOccupied && !transferItemHandler.HasKitchenItem)
        {
            if (currentKitchenItem.gameObject.TryGetComponent<ICookableItem>(out var cookableItem))
            {
                cookableItem.CancelCook(timerDisplayer);
            }

            transferItemHandler.ReceiveKitchenItem(RemoveKitchenItem());
        }

        else if (IsOccupied && transferItemHandler.HasKitchenItem)
        {
            if (transferItemHandler.GetKitchenItem.TryGetComponent<IContainerItem>(out var containerItem))
            {
                if (containerItem.CanPuttableOnPlate(currentKitchenItem))
                {
                    currentKitchenItem.TryGetComponent<ICookableItem>(out var cookable);
                    cookable.CancelCook(timerDisplayer);
                    currentKitchenItem.TryGetComponent(out IKitchenItemStateProvider stateProvider);
                    containerItem.PutOnPlate(RemoveKitchenItem(), stateProvider);
                }
            }
        }
    }
}
