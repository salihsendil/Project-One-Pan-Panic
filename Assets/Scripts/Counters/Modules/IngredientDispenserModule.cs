using UnityEngine;
using Zenject;

public class IngredientDispenserModule : MonoBehaviour, IInstantModule
{
    //Zenject
    [Inject] private UniversalPoolManager poolManager;

    //Item Data
    [SerializeField] private IngredientItemSO ingredientItemSO;

    public bool TryInteractionInstant(IInteractor interactor)
    {
        if (interactor.HasItem) return false;

        IngredientItem item = poolManager.Spawn<IngredientItem>(ingredientItemSO.Type);

        if (item == null) { return false; }

        if (!item.TryGetComponent(out IPickable pickable)) return false;

        interactor.SetItem(pickable);

        return true;
    }
}