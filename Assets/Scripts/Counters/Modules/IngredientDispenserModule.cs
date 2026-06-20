using UnityEngine;
using Zenject;

[RequireComponent(typeof(ItemSocket))]
public class IngredientDispenserModule : MonoBehaviour, IInstantModule
{
    //Zenject
    [Inject] private UniversalPoolManager poolManager;
    [Inject] private SignalBus signalBus;

    //References
    private IInteractor interactor;

    //Item Data
    [SerializeField] private IngredientItemSO ingredientItemSO;

    private void Awake()
    {
        interactor = GetComponent<IInteractor>();
    }

    public bool TryInteractionInstant(IInteractor interactor)
    {
        if (interactor.HasItem) return false;

        IngredientItem item = poolManager.Spawn<IngredientItem>(ingredientItemSO.ItemType);

        if (item == null) { return false; }

        item.transform.position = transform.position;

        interactor.SetItem(item);

        signalBus.Fire(new ItemTransferredSignal(GameplayEvent.ItemPickedUp, item.GetItemType(), this.interactor, interactor));

        return true;
    }
}