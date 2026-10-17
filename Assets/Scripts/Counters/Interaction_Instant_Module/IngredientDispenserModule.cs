using System;
using UnityEngine;
using Zenject;


// This module provides an ingredient item to the player.
// The module's interactor and the incoming interactor (which is the player in this project)
// must both be empty.
// During the interaction, it spawns an object from the prefab and gets its IPickable component.
// After that, it sets the item to the player.


public class IngredientDispenserModule : MonoBehaviour, IInstantInteractionModule
{
    private IInteractor currentInteractor;
    [Inject] private AdvancedPoolManager poolManager;
    [SerializeField] private IngredientItemSO ingredientItemSO;

    /// <summary>
    /// Invoked when an item is transferred between interactors.
    /// </summary>
    /// <remarks>
    /// Parameters: from interactor, to interactor, transferred item.
    /// </remarks>
    public event Action<IInteractor, IInteractor, IPickable> OnItemTransferred;

    private void Awake()
    {
        currentInteractor = GetComponent<IInteractor>();
    }

    public bool TryInteractInstant(IInteractor incomingInteractor)
    {
        if (currentInteractor.HasItem) return false;
        if (incomingInteractor.HasItem) return false;

        IngredientItem item = poolManager.Spawn<IngredientItem>(ingredientItemSO.ItemType);

        if (item == null) return false;

        incomingInteractor.SetItem(item);
        OnItemTransferred?.Invoke(currentInteractor, incomingInteractor, item);
        return true;
    }
}
