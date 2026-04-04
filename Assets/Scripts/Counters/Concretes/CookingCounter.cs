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

            if (!cookingModule.CanProcessable(interactor.GetItem)) return;
            itemInteractionModule.TryInteractionInstant(interactor);
            cookingModule.StartProcess(currentInteractor.GetItem);
            return;
        }

        if (currentInteractor.HasItem)
        {
            if (!interactor.HasItem)
            {
                cookingModule.StopProcess();
                itemInteractionModule.TryInteractionInstant(interactor);

                return;
            }

            if (!interactor.GetItem.GetGameObject.TryGetComponent(out IContainer container)) return;
            if (!container.CanInteractWith(currentInteractor.GetItem)) return;

            cookingModule.StopProcess();
            container.InteractWith(currentInteractor.RemoveItem());
        }
    }
}