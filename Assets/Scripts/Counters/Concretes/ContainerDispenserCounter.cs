using UnityEngine;

[RequireComponent(typeof(ItemInteractionModule))]
[RequireComponent(typeof(ContainerDispenserModule))]
public class ContainerDispenserCounter : BaseCounter
{
    private ItemInteractionModule itemInteractionModule;
    private ContainerDispenserModule containerModule;

    protected override void Awake()
    {
        itemInteractionModule = GetComponent<ItemInteractionModule>();
        containerModule = GetComponent<ContainerDispenserModule>();
    }

    public override void InteractionStarted(IInteractor interactor)
    {
        if (itemInteractionModule.TryInteractionInstant(interactor))
        {
            containerModule.TryInteractionInstant(interactor);
        }

    }
}
