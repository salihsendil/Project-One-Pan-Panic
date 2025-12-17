
public interface IInteractableAutoModule
{
    public bool IsProcessing();
    public bool CanInteractableAuto(ItemBehaviourController controller);
    public void InteractAuto(ItemBehaviourController controller);
    public bool TryInteractPause(ItemBehaviourController controller);
}
