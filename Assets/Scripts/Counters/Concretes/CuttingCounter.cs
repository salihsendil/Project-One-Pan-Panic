using UnityEngine;

[RequireComponent(typeof(ItemInteractionModule))]
[RequireComponent(typeof(CuttingModule))]
public class CuttingCounter : BaseCounter
{
    protected override void Awake()
    {
        base.Awake();
        TryGetComponent(out ItemInteractionModule itemInteractionModule);
        TryGetComponent(out CuttingModule cuttingModule);
        counterModules[0] = itemInteractionModule;
        counterModules[1] = cuttingModule;
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
}
