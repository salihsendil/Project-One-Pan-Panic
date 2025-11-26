using UnityEngine;

[RequireComponent(typeof(ItemInteractionModule))]
[RequireComponent(typeof(CuttingModule))]
public class CuttingCounter : BaseCounter, IInteractableAlternate
{
    private IInteractableAlternateModule[] alternateModules = new IInteractableAlternateModule[2];

    protected override void Awake()
    {
        base.Awake();

        TryGetComponent(out ItemInteractionModule itemInteractionModule);
        counterModules[0] = itemInteractionModule;

        TryGetComponent(out CuttingModule cuttingModule);
        alternateModules[0] = cuttingModule;
    }

    public void InteractAlternate(PlayerController playerController)
    {
        if (!itemSocket.HasItem()) { return; }

        var kitchenItem = itemSocket.GetItem();

        foreach (var module in alternateModules)
        {
            if (module == null) { continue; }

            if (module.CanInteractAlternate(kitchenItem))
            {
                module.InteractAlternate(kitchenItem, playerController);
                break;
            }
        }
    }
}
