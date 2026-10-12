

// Classes that implement this interface listen for external events
// and perform their own instant or hold interactions in response.
// They are not usually called directly by other classes.


public interface IStateModule
{
    public void Initialize(IInteractor interactor);
    public void Dispose(/*IInteractor interactor*/);
}
