
public interface IInteractableAlternateModule
{
    public bool IsProcessing();
    public bool CanInteractAlternate(ItemBehaviourController controller);
    public void InteractAlternate(ItemBehaviourController controller, PlayerController playerController);
    public void InteractPause(ItemBehaviourController controller);
    public void OnModuleProcessComplete(ItemBehaviourController controller);
}
