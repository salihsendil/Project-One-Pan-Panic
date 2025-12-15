
public interface IInteractableAlternateModule
{
    public bool IsProcessing();
    public bool CanInteractAlternate(KitchenItem kitchemItem);
    public void InteractAlternate(KitchenItem kitchenItem, PlayerController playerController);
    public void InteractPause(KitchenItem kitchenItem);
    public void OnModuleProcessComplete(KitchenItem kitchemItem);
}
