using UnityEngine;
using Zenject;

[RequireComponent(typeof(ItemSocket))]
public class ItemInteractionModule : MonoBehaviour, IInstantModule
{
    //Zenject
    [Inject] private SignalBus signalBus;

    //References
    private ItemSocket itemSocket;


    private void Awake()
    {
        itemSocket = GetComponent<ItemSocket>();
    }

    public bool TryInteractionInstant(IInteractor interactor)
    {
        if (!itemSocket.HasItem)
        {
            if (!interactor.HasItem) return false;
            if (!interactor.GetItem.IsPickable) return false;

            IPickable item = interactor.RemoveItem();
            itemSocket.SetItem(item);

            signalBus.Fire(new ItemTransferredSignal(GameplayEvent.ItemPickedUp, item.GetItemType(), interactor, itemSocket));

            return true;
        }

        if (itemSocket.HasItem)
        {
            if (!interactor.HasItem)
            {
                if (!itemSocket.GetItem.IsPickable) return false;

                IPickable item = itemSocket.RemoveItem();
                interactor.SetItem(item);

                signalBus.Fire(new ItemTransferredSignal(GameplayEvent.ItemPickedUp, item.GetItemType(), itemSocket, interactor));

                return true;
            }

            if (itemSocket.GetItem.GetGameObject.TryGetComponent(out IContainer socketItem))
            {
                if (socketItem.CanAddItem(interactor.GetItem, out IngredientItem ingredient))
                {
                    interactor.RemoveItem();
                    socketItem.AddItem(ingredient);
                    return true;
                }
            }

            if (interactor.GetItem.GetGameObject.TryGetComponent(out IContainer heldItem))
            {
                if (heldItem.CanAddItem(itemSocket.GetItem, out IngredientItem ingredient))
                {
                    itemSocket.RemoveItem();
                    heldItem.AddItem(ingredient);
                    return true;
                }
            }
        }

        return false;
    }
}