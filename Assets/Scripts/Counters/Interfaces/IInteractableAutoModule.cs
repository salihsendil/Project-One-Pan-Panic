
public interface IInteractableAutoModule
{
    public bool IsProcessing();
    public bool CanInteractableAuto(KitchenItem kitchenItem);
    public void InteractAuto(KitchenItem kitchenItem);
    public bool TryInteractPause(KitchenItem kitchenItem);
}
