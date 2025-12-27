using UnityEngine;
using Zenject;

public class IngredientDispenserModule : MonoBehaviour, IInteractableModule
{
    //References
    [Inject] private KitchenItemPoolManager poolManager;

    //Item Data
    [SerializeField] private IngredientItemSO ingredientItemSO;

    public bool TryInteract(PlayerCarryingController player)
    {
        BaseKitchenItem item = poolManager.GetItemFromPool(ingredientItemSO);

        if (item == null) { return false; }

        player.SetItem(item);

        return true;
    }
}
