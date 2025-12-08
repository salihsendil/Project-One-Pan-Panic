
public interface IInteractableAlternateModule
{
    public bool CanInteractAlternate(KitchenItem kitchemItem);
    public void InteractAlternate(KitchenItem kitchenItem, PlayerController playerController);
    public void InteractPause(KitchenItem kitchenItem);
}
