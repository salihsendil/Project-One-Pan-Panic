
public interface IInteractable
{
    public void HighlightInteractable(bool canInteractable);
    public void InteractInstant(IInteractor interactor);
    public void InteractHoldStarted(IInteractor interactor);
    public void InteractHoldCanceled(IInteractor interactor);
}
