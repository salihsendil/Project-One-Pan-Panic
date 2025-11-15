using UnityEngine;

[RequireComponent(typeof(ItemInteractionModule))]
public class HoldingCounter : BaseCounter
{
    protected override void Awake()
    {
        base.Awake();
        TryGetComponent(out ItemInteractionModule itemInteractionModule);
        counterModules[0] = itemInteractionModule;
    }

    public override void Interact(PlayerCarryingController player)
    {
        foreach (var module in counterModules)
        {
            if (module == null) { continue; }

            if (module.TryInteract(player))
            {
                break;
            }
        }
    }
}
