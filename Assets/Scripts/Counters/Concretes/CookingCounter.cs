using UnityEngine;

[RequireComponent(typeof(ItemInteractionModule))]
[RequireComponent(typeof(CookingModule))]
public class CookingCounter : BaseCounter
{
    private IInteractor currentInteractor;
    private ItemInteractionModule itemInteractionModule;
    private CookingModule cookingModule;

    protected override void Awake()
    {
        currentInteractor = GetComponent<ItemSocket>();
        itemInteractionModule = GetComponent<ItemInteractionModule>();
        cookingModule = GetComponent<CookingModule>();
        base.Awake();
    }

    public override void InteractionStarted(IInteractor interactor)
    {
        if (!currentInteractor.HasItem)
        {
            if (!interactor.HasItem) return;

            if (cookingModule.CanProcessable(interactor.GetItem))
            {
                base.InteractionStarted(interactor);
            }

            return;
        }

        else //currentInteractor.HasItem
        {
            if (interactor.HasItem && interactor.GetItem is IContainer container)
            {
                if (!container.CanAddItem(currentInteractor.GetItem, out IngredientItem _)) return;
            }

            cookingModule.PauseProcess();
            itemInteractionModule.TryInteractionInstant(interactor);
        }
    }
}