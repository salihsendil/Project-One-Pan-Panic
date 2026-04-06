
public interface IInteractable
{
    public void HighlightInteractable(bool canInteractable);
    public void InteractionStarted(IInteractor interactor);
    public void InteractionPerformed(IInteractor interactor);
    public void InteractionCanceled(IInteractor interactor);
}
