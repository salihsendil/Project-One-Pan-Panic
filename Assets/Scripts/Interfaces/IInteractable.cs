
public interface IInteractable
{
    public void InteractionStarted(IInteractor interactor);
    public void InteractionPerformed(IInteractor interactor);
    public void InteractionCanceled(IInteractor interactor);
}
