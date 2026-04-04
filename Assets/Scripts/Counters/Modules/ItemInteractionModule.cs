// ItemInteractionModule.cs
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
        if (!itemSocket.HasItem)
        {
            if (!interactor.HasItem) return false;
            if (!interactor.GetItem.IsPickable) return false;

            itemSocket.SetItem(interactor.RemoveItem());
            return true;
        }

        if (itemSocket.HasItem)
        {
            if (!interactor.HasItem)
            {
                if (!itemSocket.GetItem.IsPickable) return false;

                interactor.SetItem(itemSocket.RemoveItem());
                return true;
            }

            itemSocket.GetItem.GetGameObject.TryGetComponent(out IContainer socketContainer);

            if (socketContainer != null && socketContainer.CanInteractWith(interactor.GetItem))
            {
                socketContainer.InteractWith(interactor.RemoveItem());
                return true;
            }

            interactor.GetItem.GetGameObject.TryGetComponent(out IContainer interactorContainer);

            if (interactorContainer != null && interactorContainer.CanInteractWith(itemSocket.GetItem))
            {
                interactorContainer.InteractWith(itemSocket.RemoveItem());
                return true;
            }
        }

        return false;
    }
}