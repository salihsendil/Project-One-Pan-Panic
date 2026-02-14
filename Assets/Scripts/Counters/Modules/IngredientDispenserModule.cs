using UnityEngine;
using Zenject;

public class IngredientDispenserModule : MonoBehaviour, IInteractableModule
{
    //References
    [Inject] private UniversalPoolManager poolManager;

    //Item Data
    [SerializeField] private IngredientItemSO ingredientItemSO;

    public bool TryInteract(PlayerCarryingController player)
    {
        IngredientItem item = poolManager.Spawn<IngredientItem>(ingredientItemSO.Type);

        if (item == null) { return false; }

        player.SetItem(item);

        return true;
    }
}
