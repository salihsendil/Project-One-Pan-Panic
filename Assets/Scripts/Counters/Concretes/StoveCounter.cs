using UnityEngine;


// Main class of the Stove Counter. It only accepts cookware items.


[RequireComponent(typeof(ItemInteractionModule))]
[RequireComponent(typeof(HeatingModule))]
public class StoveCounter : BaseCounter
{
    private IInteractor currentInteractor;

    private void OnEnable()
    {
        currentInteractor = GetComponent<IInteractor>();

        foreach (var module in stateModules)
            module.Initialize(currentInteractor);
    }

    private void OnDisable()
    {
        foreach (var module in stateModules)
            module.Dispose();
    }

    public override void InteractInstant(IInteractor incomingInteractor)
    {
        if (!currentInteractor.HasItem)
        {
            if (!incomingInteractor.HasItem) return;
            if (!incomingInteractor.GetItem.Transform.TryGetComponent(out CookwareItem _)) return;
        }

        base.InteractInstant(incomingInteractor);
    }
}