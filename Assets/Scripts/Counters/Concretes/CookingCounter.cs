using UnityEngine;

[RequireComponent(typeof(ItemInteractionModule))]
[RequireComponent(typeof(CookingModule))]
public class CookingCounter : BaseCounter
{
    private ItemSocket itemSocket;
    private CookingModule cookingModule;

    private void Awake()
    {
        TryGetComponent(out itemSocket);

        TryGetComponent(out ItemInteractionModule itemInteractionModule);
        counterModules[0] = itemInteractionModule;

        TryGetComponent(out cookingModule);
    }

    public override void Interact(PlayerCarryingController player)
    {
        if (!player.HasItem())
        {
            if (!itemSocket.HasItem()) { return; }

            if (!cookingModule.IsProcessing())
            {
                base.Interact(player);
                return;
            }

            BaseKitchenItem kitchenItem = itemSocket.GetItem();

            if (!kitchenItem.TryGetBehaviourController(out ItemBehaviourController controller)) { return; }

            else if (cookingModule.TryInteractPause(controller))
            {
                base.Interact(player);
            }
        }

        else //player.HasItem()
        {
            BaseKitchenItem kitchenItem = player.GetItem();

            if (!itemSocket.HasItem())
            {
                if (!kitchenItem.TryGetBehaviourController(out ItemBehaviourController controller)) { return; }

                if (cookingModule.CanInteractableAuto(controller))
                {
                    base.Interact(player);
                    cookingModule.InteractAuto(controller);
                    return;
                }
            }

            else if (kitchenItem is ContainerItem containerItem)
            {
                if (containerItem.TryInteractWith(itemSocket.GetItem()))
                {
                    if (!itemSocket.GetItem().TryGetBehaviourController(out ItemBehaviourController controller)) { return; }

                    if (cookingModule.TryInteractPause(controller))
                    {
                        base.Interact(player);
                    }
                }
            }
        }
    }
}