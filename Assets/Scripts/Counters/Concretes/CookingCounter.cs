using UnityEngine;

[RequireComponent(typeof(CookingModule))]
public class CookingCounter : BaseCounter
{
    protected override void Awake()
    {
        base.Awake();
        TryGetComponent(out CookingModule cookingModule);
        counterModules[1] = cookingModule;
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
