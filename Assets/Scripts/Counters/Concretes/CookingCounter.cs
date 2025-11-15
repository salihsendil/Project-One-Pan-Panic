using UnityEngine;

[RequireComponent(typeof(ItemInteractionModule))]
[RequireComponent(typeof(CookingModule))]
public class CookingCounter : BaseCounter
{
    protected override void Awake()
    {
        base.Awake();
        TryGetComponent(out ItemInteractionModule itemInteractionModule);
        TryGetComponent(out CookingModule cookingModule);
        counterModules[0] = itemInteractionModule;
        counterModules[1] = cookingModule;
    }
    public override void Interact(PlayerCarryingController player)
    {
        foreach (var module in counterModules)
        {
            if (module.TryInteract(player))
            {
                //if (!cancookable()){continue;}
                break;
            }
        }
    }
}
