using UnityEngine;

[RequireComponent(typeof(IngredientDispenserModule))]
public class IngredientDispenserCounter : BaseCounter
{
    [SerializeField] private KitchenItemSO itemSO;

    protected override void Awake()
    {
        base.Awake();
        TryGetComponent(out IngredientDispenserModule ingredientDispenserModule);
        counterModules[1] = ingredientDispenserModule;
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
