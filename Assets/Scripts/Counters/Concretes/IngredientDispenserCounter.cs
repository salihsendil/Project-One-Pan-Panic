using UnityEngine;

[RequireComponent(typeof(ItemInteractionModule))]
[RequireComponent(typeof(IngredientDispenserModule))]
public class IngredientDispenserCounter : BaseCounter
{
    [SerializeField] private KitchenItemSO itemSO;

    protected override void Awake()
    {
        base.Awake();
        TryGetComponent(out ItemInteractionModule itemInteractionModule);
        TryGetComponent(out IngredientDispenserModule ingredientDispenserModule);
        counterModules[0] = itemInteractionModule;
        counterModules[1] = ingredientDispenserModule;
    }
}
