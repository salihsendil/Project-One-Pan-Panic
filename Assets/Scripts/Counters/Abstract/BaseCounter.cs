using UnityEngine;

[RequireComponent(typeof(CounterHighlighter))]
public abstract class BaseCounter : MonoBehaviour, IInteractable
{
    protected IInstantModule[] instantModules = new IInstantModule[3];
    protected IHoldModule[] holdModules = new IHoldModule[2];

    protected virtual void Awake()
    {
        instantModules = GetComponents<IInstantModule>();
        holdModules = GetComponents<IHoldModule>();
    }

    public virtual void InteractionStarted(IInteractor interactor)
    {
        foreach (var module in instantModules)
        {
            module?.TryInteractionInstant(interactor);
        }

        foreach (var module in holdModules)
        {
            module?.OnInteractionStarted();
        }
    }

    public virtual void InteractionPerformed(IInteractor interactor)
    {
        foreach (var module in holdModules)
        {
            module?.OnInteractionPerformed();
        }
    }

    public virtual void InteractionCanceled(IInteractor interactor)
    {
        foreach (var module in holdModules)
        {
            module?.OnInteractionCanceled();
        }
    }
}
