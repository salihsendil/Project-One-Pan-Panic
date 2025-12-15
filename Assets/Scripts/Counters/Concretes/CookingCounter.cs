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
        if (!player.HasItem())
        {
            if (!itemSocket.HasItem()) { return; }

            else if (!cookingModule.IsProcessing())
            {
                base.Interact(player);
                return;
            }

            else if (cookingModule.TryInteractPause(itemSocket.GetItem()))
            {
                base.Interact(player);
            }
        }

        else //player.HasItem()
        {
            if (!itemSocket.HasItem())
            {
                if (cookingModule.CanInteractableAuto(player.GetItem()))
                {
                    base.Interact(player);
                    cookingModule.InteractAuto(itemSocket.GetItem());
                    return;
                }
            }

            Debug.Log("Player plate state");
            //if(tryinteract)
            //pauseprocess
            //interact
        }
    }
}