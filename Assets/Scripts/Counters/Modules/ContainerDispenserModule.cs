using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(ItemSocket))]
public class ContainerDispenserModule : MonoBehaviour, IInstantModule
{
    //Zenject
    [Inject] private UniversalPoolManager poolManager;
    [Inject] private SignalBus signalBus;

    //References
    private IInteractor itemSocket;

    //Data
    [SerializeField] private ContainerItemSO containerData;

    //Module Variables
    private Stack<ContainerItem> containerStack = new();
    [SerializeField] private Vector3 offsetVector = new Vector3(0f, 0.05f, 0f);

    private void Awake()
    {
        itemSocket = GetComponent<IInteractor>();
    }

    #region Signal Subscription
    private void OnEnable()
    {
        signalBus.Subscribe<ContainerItemDespawnSignal>(ContainerItemDespawned);
    }
    private void OnDisable()
    {
        signalBus.Unsubscribe<ContainerItemDespawnSignal>(ContainerItemDespawned);
    }
    #endregion

    private void Start()
    {
        while (TryPlaceContainerToStack()) { }
    }

    private async void ContainerItemDespawned()
    {
        await Task.Delay(1500);

        TryPlaceContainerToStack();
    }

    private bool TryPlaceContainerToStack()
    {
        ContainerItem container = poolManager.Spawn<ContainerItem>( containerData.ItemType);

        if (container == null) return false;

        container.transform.position = transform.position;
        containerStack.Push(container);
        itemSocket.SetItemToOffset(container, containerStack.Count * offsetVector);
        return true;
    }

    public bool TryInteractionInstant(IInteractor interactor)
    {
        if (containerStack == null || containerStack.Count <= 0) return false;

        if (!interactor.HasItem)
        {
            IPickable item = itemSocket.RemoveItem();
            interactor.SetItem(item);
            containerStack.Pop();

            if (containerStack.TryPeek(out ContainerItem container))
            {
                itemSocket.SetItemToOffset(container, containerStack.Count * offsetVector); //fix required
            }

            signalBus.Fire(new ItemTransferredSignal(GameplayEvent.ItemPickedUp, item.GetItemType(), itemSocket, interactor));
        }

        else
        {
            if (interactor.GetItem.GetGameObject.TryGetComponent(out IContainer _)) return false;

            if (!containerStack.Peek().CanAddItem(interactor.GetItem, out IngredientItem ingredient)) return false;

            interactor.RemoveItem();
            ContainerItem container = containerStack.Pop();

            container.AddItem(ingredient);
            interactor.SetItem(itemSocket.RemoveItem());

            if (containerStack.TryPeek(out ContainerItem item))
            {
                itemSocket.SetItemToOffset(item, containerStack.Count * offsetVector); //fix required
            }

            signalBus.Fire(new ItemTransferredSignal(GameplayEvent.ItemPickedUp, container.GetItemType(), itemSocket, interactor));
        }

        return true;
    }
}
