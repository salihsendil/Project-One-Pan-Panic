
using System.Collections;

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
    public void StartProcess(IPickable pickable);
    public void StopProcess();
    public void CompleteProcess();
}