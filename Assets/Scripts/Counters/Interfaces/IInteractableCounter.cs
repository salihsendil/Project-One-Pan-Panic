

// All interactable counters must implement this interface.
// The player looks for this interface to call the interaction methods.


public interface IInteractableCounter
{
    public bool IsInteractionEnabled { get; set; }
    public void HighlightInteractable(bool canInteractable);
    public void InteractInstant(IInteractor interactor);
    public void InteractHoldStarted(IInteractor interactor);
    public void InteractHoldCanceled(IInteractor interactor);
}