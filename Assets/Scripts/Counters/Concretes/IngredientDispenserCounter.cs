using UnityEngine;


// This class gives an item to the player. It can be used like a Holding Counter.
// In this class, modules should be called in the correct order.
// If the IngredientDispenserModule interaction method returns true,
// the ItemInteractionModule method will not be called.


[RequireComponent(typeof(ItemInteractionModule))]
[RequireComponent(typeof(IngredientDispenserModule))]
public class IngredientDispenserCounter : BaseCounter
{
    protected override void Awake()
    {
        counterHighlighter = GetComponent<CounterHighlighter>();

        instantInteractionModules[0] = GetComponent<ItemInteractionModule>();
        instantInteractionModules[1] = GetComponent<IngredientDispenserModule>();
    }


    public override void InteractInstant(IInteractor incomingInteractor)
    {
        foreach (var module in instantInteractionModules)
        {
            if (module == null) continue;

            if (module.TryInteractInstant(incomingInteractor))
            {
                break;
            }
        }
    }

}
