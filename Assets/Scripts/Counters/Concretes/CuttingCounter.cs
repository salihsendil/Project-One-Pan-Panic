using UnityEngine;


// This is the main class of the Cutting Counter. When creating a Cutting Counter GameObject, all you need to do is add this.
// Every script you need will come with it.
// It requires ItemInteractionModule to set, remove, and interact with items.
// Every type of item can be placed, but not all of them can be processed.
//
// If ItemSocket has an item and it is not pickable (when the cutting process starts,
// the item is locked to the counter and cannot be picked up until the process is finished),
// the Player cannot make any interactions.
//
// If the Player ItemSocket has an item, the player cannot process anything.
// The player can only try to set or remove an item.


[RequireComponent(typeof(ItemInteractionModule))]
[RequireComponent(typeof(CuttingModule))]
public class CuttingCounter : BaseCounter
{
    private IInteractor interactor;

    private void Start()
    {
        interactor = GetComponent<IInteractor>();
    }

    public override void InteractInstant(IInteractor incomingInteractor)
    {
        if (interactor.HasItem && !interactor.GetItem.IsPickable) return;

        base.InteractInstant(incomingInteractor);
    }

    public override void InteractHoldStarted(IInteractor incomingInteractor)
    {
        if (incomingInteractor.HasItem) return;

        base.InteractHoldStarted(incomingInteractor);
    }

    public override void InteractHoldCanceled(IInteractor incomingInteractor)
    {
        base.InteractHoldCanceled(incomingInteractor);
    }
}