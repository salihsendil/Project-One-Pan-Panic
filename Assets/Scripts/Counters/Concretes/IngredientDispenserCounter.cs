using UnityEngine;

[RequireComponent(typeof(ItemInteractionModule))]
[RequireComponent(typeof(IngredientDispenserModule))]
public class IngredientDispenserCounter : BaseCounter
{
    public override void InteractionStarted(IInteractor interactor)
    {
        foreach (var module in instantModules)
        {
            if (module == null) { return; }

            if (module.TryInteractionInstant(interactor))
            {
                break;
            }
        }
    }
}
