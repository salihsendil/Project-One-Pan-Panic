using UnityEngine;

[RequireComponent(typeof(CounterHighlighter))]
public abstract class BaseCounter : MonoBehaviour, IInteractable
{
    protected CounterHighlighter counterHighlighter;

    protected IInstantModule[] instantModules = new IInstantModule[3];
    protected IHoldModule[] holdModules = new IHoldModule[2];
    protected IAutoModule[] autoModules = new IAutoModule[2];

    protected virtual void Awake()
    {
        instantModules = GetComponents<IInstantModule>();
        holdModules = GetComponents<IHoldModule>();
        autoModules = GetComponents<IAutoModule>();
    }
    private void OnEnable()
    {
        counterHighlighter = GetComponent<CounterHighlighter>();
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

        foreach (var module in autoModules)
        {
            module?.StartProcess();
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

    public void HighlightInteractable(bool canInteractable)
    {
        counterHighlighter.HighlightObject(canInteractable);
    }
}
