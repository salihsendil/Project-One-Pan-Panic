using UnityEngine;

[RequireComponent(typeof(PlateDispenserModule))]
public class PlateDispenserCounter : BaseCounter
{
    protected override void Awake()
    {
        base.Awake();
        TryGetComponent(out PlateDispenserModule plateDispenserModule);
        counterModules[0] = plateDispenserModule;
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
