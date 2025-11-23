using UnityEngine;

[RequireComponent(typeof(ItemInteractionModule))]
[RequireComponent(typeof(CookingModule))]
public class CookingCounter : BaseCounter
{
    private IInteractableAlternateModule[] alternateModules = new IInteractableAlternateModule[2];

    protected override void Awake()
    {
        base.Awake();

        TryGetComponent(out ItemInteractionModule itemInteractionModule);
        counterModules[0] = itemInteractionModule;

        TryGetComponent(out CookingModule cookingModule);
        alternateModules[0] = cookingModule;
    }

    public override void Interact(PlayerCarryingController player)
    {
        var kitchenItem = player.HasItem() ? player.GetItem() : itemSocket.GetItem();

        if (kitchenItem == null) { return; }

        GetInteractableAlternateModule(kitchenItem, out IInteractableAlternateModule alternateModule);

        foreach (var module in counterModules)
        {
            if (module == null) { return; }
            if (module.TryInteract(player))
            {
                alternateModule?.InteractAlternate(kitchenItem);
                break;
            }
        }
    }

    private void GetInteractableAlternateModule(KitchenItem kitchenItem, out IInteractableAlternateModule module)
    {
        IInteractableAlternateModule alternateModule = null;

        foreach (var alternate in alternateModules)
        {
            if (alternate == null) { continue; }

            if (alternate.CanInteractAlternate(kitchenItem))
            {
                alternateModule = alternate;
                break;
            }
        }
        module = alternateModule;
    }
}
