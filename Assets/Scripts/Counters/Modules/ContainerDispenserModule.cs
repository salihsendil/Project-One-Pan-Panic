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
    private ItemSocket itemSocket;

    //Data
    [SerializeField] private ContainerItemSO containerItemSO;

    //Stack
    private Stack<ContainerItem> containerStack = new();

    //Transform
    private Vector3 positionOffset = new Vector3(0f, 0.05f, 0f);

    //Respawn Delay
    [SerializeField] private int respawnDelay = 1500;

    private void Awake()
    {
        itemSocket = GetComponent<ItemSocket>();
    }

    private void OnEnable()
    {
        signalBus.Subscribe<ContainerItemDespawned>(PrepareReplacement);
    }

    private void OnDisable()
    {
        signalBus.Unsubscribe<ContainerItemDespawned>(PrepareReplacement);
    }

    private async void PrepareReplacement()
    {
        await Task.Delay(respawnDelay);

        ContainerItem item = poolManager.Spawn<ContainerItem>(containerItemSO.PoolType);
        if (item == null) return;

        if (!item.TryGetComponent(out IPickable pickable)) return;

        itemSocket.SetItemToOffset(pickable, positionOffset * containerStack.Count);
        containerStack.Push(item);
    }

    public bool TryInteract(IInteractor interactor)
    {
        if (containerStack == null || containerStack.Count <= 0) { return false; }

        if (interactor.HasItem)
        {
            if (!itemSocket.GetItem.GetGameObject.TryGetComponent(out BaseKitchenItem item)) { return false; }
            if (!interactor.GetItem.GetGameObject.TryGetComponent(out BaseKitchenItem playerItem)) { return false; }
            if (!item.TryInteractWith(playerItem)) { return false; }

            interactor.RemoveItem();
            return true;
        }

        else //!player.HasItem()
        {
            if (!containerStack.TryPop(out ContainerItem containerItem)) { return false; }

            if (!containerItem.TryGetComponent(out IPickable pickable)) return false;

            interactor.SetItem(pickable);
            itemSocket.RemoveItem();

            if (containerStack.TryPeek(out ContainerItem container))
            {
                itemSocket.SetItemToOffset(pickable, positionOffset * (containerStack.Count - 1));
            }

            return true;
        }
    }

    public bool TryInteractionInstant(IInteractor interactor)
    {
        throw new System.NotImplementedException();
    }
}
