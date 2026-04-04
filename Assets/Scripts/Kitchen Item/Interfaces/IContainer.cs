public interface IContainer
{
    public IInteractor GetInteractor { get; }

    public bool CanInteractWith(IPickable pickable);
    public void InteractWith(IPickable pickable);
}
