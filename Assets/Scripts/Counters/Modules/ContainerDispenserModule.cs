using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(ItemSocket))]
public class ContainerDispenserModule : MonoBehaviour, IInteractableModule
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
        if (itemSocket == null) { TryGetComponent(out itemSocket); }
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

        ContainerItem item = poolManager.Spawn<ContainerItem>(containerItemSO.Type);
        if (item == null) { return; }

        itemSocket.SetItemToOffset(item, positionOffset * containerStack.Count);
        containerStack.Push(item);
    }

    public bool TryInteract(PlayerCarryingController player)
    {
        if (containerStack == null || containerStack.Count <= 0) { return false; }

        if (player.HasItem())
        {
            if (!itemSocket.GetItem().TryInteractWith(player.GetItem())) { return false; }

            player.RemoveItem();
            return true;
        }

        else //!player.HasItem()
        {
            if (!containerStack.TryPop(out ContainerItem containerItem)) { return false; }

            player.SetItem(containerItem);
            itemSocket.RemoveItem();

            if (containerStack.TryPeek(out ContainerItem container))
            {
                itemSocket.SetItemToOffset(container, positionOffset * (containerStack.Count - 1));
            }

            return true;
        }
    }
}
