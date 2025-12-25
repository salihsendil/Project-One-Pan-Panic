using UnityEngine;
using Zenject;

public class IngredientDispenserModule : MonoBehaviour, IInteractableModule
{
    //References
    [Inject] private KitchenItemPoolManager poolManager;

    //Item Data
    [SerializeField] private KitchenItemSO kitchenItemSO;

    public bool TryInteract(PlayerCarryingController player)
    {
        BaseKitchenItem item = poolManager.GetItemFromPool(kitchenItemSO);

        if (item == null) { return false; }

        player.SetItem(item);

        return true;
    }
}
