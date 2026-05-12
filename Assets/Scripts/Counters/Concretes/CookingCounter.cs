using UnityEngine;

[RequireComponent(typeof(ItemInteractionModule))]
[RequireComponent(typeof(CookingModule))]
public class CookingCounter : BaseCounter
{
    private IInteractor itemSocket;
    private ItemInteractionModule itemInteractionModule;
    private CookingModule cookingModule;

    protected override void Awake()
    {
        itemSocket = GetComponent<ItemSocket>();
        itemInteractionModule = GetComponent<ItemInteractionModule>();
        cookingModule = GetComponent<CookingModule>();
        base.Awake();
    }

    public override void InteractionStarted(IInteractor interactor)
    {
        if (!itemSocket.HasItem)
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
            if (!interactor.HasItem)
            {
                cookingModule.PauseProcess();
                itemInteractionModule.TryInteractionInstant(interactor);
            }

            if (interactor.HasItem && interactor.GetItem is IContainer container)
            {
                if (!container.CanAddItem(itemSocket.GetItem, out IngredientItem _)) return;

                cookingModule.PauseProcess();
                itemInteractionModule.TryInteractionInstant(interactor);
            }
        }
    }
}