
public interface IInteractableAutoModule
{
    public bool CanInteractableAuto(KitchenItem kitchenItem);
    public void TryInteractAuto(KitchenItem kitchenItem);
    public bool TryInteractPause(KitchenItem kitchenItem);
}
