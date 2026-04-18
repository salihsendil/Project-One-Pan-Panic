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

        IngredientItem item = poolManager.Spawn<IngredientItem>(ingredientItemSO.PoolType);

        if (item == null) { return false; }

        item.transform.position = transform.position;

        interactor.SetItem(item);

        return true;
    }
}