

// This contract ensures that the item supports hold interactions.


public interface IHoldInteractionModule
{
    public void OnHoldInteractionStarted();
    public void OnHoldInteractionCanceled();
}
