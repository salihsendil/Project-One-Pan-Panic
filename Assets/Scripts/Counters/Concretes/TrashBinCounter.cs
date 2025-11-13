using UnityEngine;

[RequireComponent(typeof(TrashModule))]
public class TrashBinCounter : BaseCounter
{

    protected override void Awake()
    {
        TryGetComponent(out TrashModule trashModule);
        counterModules[0] = trashModule;
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
