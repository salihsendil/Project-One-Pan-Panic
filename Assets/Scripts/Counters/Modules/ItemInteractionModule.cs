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