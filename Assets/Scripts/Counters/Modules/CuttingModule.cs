using UnityEngine;

public class CuttingModule : MonoBehaviour, IInteractableAlternateModule
{
    private ProcessType processType = ProcessType.Cut;

    public bool CanInteractAlternate(KitchenItem kitchenItem)
    {
        if (kitchenItem.CanProcess(processType)) { return true; }
        return false;
    }

    public void InteractAlternate(KitchenItem kitchenItem, PlayerController playerController)
    {
        if (kitchenItem.TryHandleProcess(processType))
        {
            kitchenItem.UpdatePlayerBusyState(playerController);
        }
    }
}
