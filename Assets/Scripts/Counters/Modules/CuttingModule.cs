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
        if (kitchenItem.TryStartProcess(processType))
        {
            kitchenItem.UpdatePlayerBusyState(playerController);
        }

        else
        {
            InteractPause(kitchenItem);
        }
    }

    public void InteractPause(KitchenItem kitchenItem)
    {
        kitchenItem.TryPauseProcess(processType);
    }
}
