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

public interface IAutoModule : ICounterModule
{
    public bool CanProcessable(IPickable pickable);
    public void StartProcess();
    public void PauseProcess();
    public void CompleteProcess(IItemProcess process);
}