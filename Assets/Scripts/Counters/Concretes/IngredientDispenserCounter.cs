using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ItemInteractionModule))]
[RequireComponent(typeof(IngredientDispenserModule))]
public class IngredientDispenserCounter : BaseCounter
{
    private ItemSocket itemSocket;
    private void Awake()
    {
        TryGetComponent(out ItemInteractionModule itemInteractionModule);
        TryGetComponent(out IngredientDispenserModule ingredientDispenserModule);
        counterModules[0] = itemInteractionModule;
        counterModules[1] = ingredientDispenserModule;
    }

    public override bool TryGetItemIcon()
    {
        if (!itemSocket.HasItem()) { return false; }

        if (!itemSocket.GetItem().TryGetComponent(out IInfoProvider provider))
        {
            return false;
        }
        provider.GetDataInfo();

        return true;
    }
}
