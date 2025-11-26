using UnityEngine;

[RequireComponent(typeof(ItemInteractionModule))]
[RequireComponent(typeof(CookingModule))]
public class CookingCounter : BaseCounter
{
    private CookingModule cookingModule;

    protected override void Awake()
    {
        base.Awake();

        TryGetComponent(out ItemInteractionModule itemInteractionModule);
        counterModules[0] = itemInteractionModule;

        TryGetComponent(out cookingModule);
    }


    public override void Interact(PlayerCarryingController player)
    {
        bool hasItem = itemSocket.HasItem();
        bool playerHasItem = player.HasItem();

        if (hasItem)
        {
            cookingModule.TryInteractAuto(itemSocket.GetItem());
            base.Interact(player);
            return;
        }

        if (!playerHasItem) { return; }

        else
        {
            if (cookingModule.CanInteractableAuto(player.GetItem()))
            {
                base.Interact(player);
                cookingModule.TryInteractAuto(itemSocket.GetItem());
            }
        }
    }
}
