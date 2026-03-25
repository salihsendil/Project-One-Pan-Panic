using UnityEngine;

[RequireComponent(typeof(ItemSocket))]
public class ItemInteractionModule : MonoBehaviour, IInstantModule
{
    private ItemSocket itemSocket;

    private void Awake()
    {
        itemSocket = GetComponent<ItemSocket>();
    }

    public bool TryInteractionInstant(IInteractor interactor)
    {

        if (interactor.HasItem && !interactor.GetItem.IsPickable)
        {
            return false;
        }

        if (itemSocket.HasItem && !itemSocket.GetItem.IsPickable)
        {
            return false;
        }

        else if (!interactor.HasItem)
        {
            if (itemSocket.HasItem)
            {
                interactor.SetItem(itemSocket.RemoveItem());
                return true;
            }
        }

        else if (!itemSocket.HasItem)
        {
            if (interactor.HasItem)
            {
                itemSocket.SetItem(interactor.RemoveItem());
                return true;
            }
        }
        return false;
    }
}
