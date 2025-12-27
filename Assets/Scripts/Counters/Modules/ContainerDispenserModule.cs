using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(ItemSocket))]
public class ContainerDispenserModule : MonoBehaviour, IInteractableModule
{
    //Zenject
    [Inject] private KitchenItemPoolManager poolManager;

    //References
    private ItemSocket itemSocket;

    //Data
    [SerializeField] private ContainerItemSO containerItemSO;

    //Stack
    private Stack<ContainerItem> containerStack = new();

    //Container Count
    [SerializeField] private int maxCounterSize;

    //Transform
    private Vector3 positionOffset = new Vector3(0f, 0.05f, 0f);

    private void Awake()
    {
        TryGetComponent(out itemSocket);
    }

    private void Start()
    {
        for (int i = 0; i < maxCounterSize; i++)
        {
            BaseKitchenItem item = poolManager.GetItemFromPool(containerItemSO);
            if (item == null) { break; }

            itemSocket.SetItemToOffset(item, positionOffset * i);
            containerStack.Push(item as ContainerItem);
        }
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
