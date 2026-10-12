using System;
using UnityEngine;


// This is the item transfer decision-making module.
// It requires an ItemSocket (IInteractor).
// A lot of item transfer possibilities have been implemented.
// Some of them are:
// Player to counter, counter to player, player to container, counter to container,
// cookware to plate, plate to plate.
//
// !!! Cookware to cookware and plate to cookware are not supported for now.
//
// The module tries to set or remove an item from the ItemSocket depending on its situation.
// The important thing is that if both interactors are full, the module checks whether their items are containers or not.
// In this situation, the module tries to transfer items between containers or transfer an item to a container.


[RequireComponent(typeof(ItemSocket))]
public class ItemInteractionModule : MonoBehaviour, IInstantInteractionModule
{
    private IInteractor currentInteractor;

    /// <summary>
    /// Invoked when an item is transferred between interactors.
    /// </summary>
    /// <remarks>
    /// Parameters: from interactor, to interactor, transferred item.
    /// </remarks>
    public event Action<IInteractor, IInteractor, IPickable> OnItemTransferred;

    ///// <summary>
    ///// Invoked when an item is added to containers.
    ///// </summary>
    ///// <remarks>
    ///// Parameters: target container, transferred item.
    ///// </remarks>
    //public event Action<ContainerItem, IPickable> OnItemAddedToContainer;
    //public event Action<ContainerItem, ContainerItem, List<ContainerSlot>> OnContainerTransferred;


    private void Awake()
    {
        currentInteractor = GetComponent<IInteractor>();
    }

    public bool TryInteractInstant(IInteractor incomingInteractor)
    {
        if (!currentInteractor.HasItem)
        {
            if (!incomingInteractor.HasItem) return false;

            IPickable item = incomingInteractor.RemoveItem();
            currentInteractor.SetItem(item);
            OnItemTransferred?.Invoke(incomingInteractor, currentInteractor, item);
            return true;
        }

        else //currentInteractor.HasItem
        {
            if (!incomingInteractor.HasItem)
            {
                IPickable item = currentInteractor.RemoveItem();
                incomingInteractor.SetItem(item);
                OnItemTransferred?.Invoke(currentInteractor, incomingInteractor, item);
                return true;
            }

            Debug.Log("plate state");

            currentInteractor.GetItem.Transform.TryGetComponent(out ContainerItem currentContainer);
            incomingInteractor.GetItem.Transform.TryGetComponent(out ContainerItem incomingContainer);

            if (currentContainer != null)
            {
                if (incomingContainer != null)
                {
                    Debug.Log("ikisi de container");

                    if (incomingContainer.HasItems && incomingContainer.CanExportItems() && currentContainer.CanImportItems(incomingContainer.GetItems))
                    {
                        currentContainer.ImportItems(incomingContainer.ExportItems());
                        return true;
                    }

                    else if (currentContainer.HasItems && currentContainer.CanExportItems() && incomingContainer.CanImportItems(currentContainer.GetItems))
                    {
                        incomingContainer.ImportItems(currentContainer.ExportItems());
                        return true;
                    }
                    return false;
                }

                else
                {
                    Debug.Log("current container incoming ingredient");

                    IPickable incomingItem = incomingInteractor.GetItem;
                    if (currentContainer.CanReceive(incomingItem))
                    {
                        IPickable item = incomingInteractor.RemoveItem();
                        currentContainer.Receive(item);
                        //OnItemAddedToContainer?.Invoke(currentContainer, item);
                    }
                }
            }

            else //currentContainer == null
            {
                if (incomingContainer == null) return false;

                IPickable currentItem = currentInteractor.GetItem;
                if (incomingContainer.CanReceive(currentItem))
                {
                    IPickable item = currentInteractor.RemoveItem();
                    incomingContainer.Receive(item);
                    //OnItemAddedToContainer?.Invoke(incomingContainer, item);
                }
            }

            return false;
        }
    }
}