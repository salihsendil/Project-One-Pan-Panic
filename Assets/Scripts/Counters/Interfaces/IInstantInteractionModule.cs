

// This contract ensures that the relevant module supports instant interaction.
// The method does not guarantee that the interaction will be successful.
// It returns false if the interaction fails.

public interface IInstantInteractionModule
{
    public bool TryInteractInstant(IInteractor incomingInteractor);
}