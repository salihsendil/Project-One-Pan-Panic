
public interface ICounterModule { }

public interface IInstantModule : ICounterModule
{
    public bool TryInteractionInstant(IInteractor interactor);
}

public interface IHoldModule : ICounterModule
{
    public void OnInteractionStarted();
    public void OnInteractionPerformed();
    public void OnInteractionCanceled();
}