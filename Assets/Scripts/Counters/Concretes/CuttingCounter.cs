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

    public override void Interact(PlayerCarryingController player)
    {
        foreach (var module in counterModules)
        {
            if (module.TryInteract(player))
            {
                break;
            }
        }
    }

    public void InteractAlternate()
    {
        if (!itemSocket.HasItem()) { return; }

        foreach (var module in alternateModules)
        {

            if (module.TryInteractAlternate(itemSocket.GetItem()))
            {
                break;
            }
        }
    }
}
