using UnityEngine;

[RequireComponent(typeof(ItemSocket))]
[RequireComponent(typeof(CuttingModule))]
public class CuttingCounter : BaseCounter
{
    public override void InteractionStarted(IInteractor interactor)
    {
        foreach (var module in instantModules)
        {
            module?.TryInteractionInstant(interactor);
        }
    }

    public override void InteractionPerformed(IInteractor interactor)
    {
        foreach (var module in holdModules)
        {
            module?.OnInteractionStarted();
            module?.OnInteractionPerformed();
        }
    }
}
