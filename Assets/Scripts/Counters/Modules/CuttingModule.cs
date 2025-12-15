using UnityEngine;

public class CuttingModule : MonoBehaviour, IInteractableAlternateModule
{
    private bool isProcessing;
    public bool IsProcessing() => isProcessing;

    private ProcessType processType = ProcessType.Cut;

    public bool CanInteractAlternate(KitchenItem kitchenItem)
    {
        if (kitchenItem.CanProcess(processType)) { return true; }
        return false;
    }

    public void InteractAlternate(KitchenItem kitchenItem, PlayerController playerController)
    {
        if (kitchenItem.WorkStage == WorkStage.Paused)
        {
            kitchenItem.HandleResumeProcess(processType);
        }

        else
        {
            kitchenItem.HandleProcessStart(processType);
        }

        kitchenItem.OnItemProcessComplete += OnModuleProcessComplete;
        isProcessing = true;
    }

    public void InteractPause(KitchenItem kitchenItem)
    {
        kitchenItem.OnItemProcessComplete -= OnModuleProcessComplete;
        kitchenItem.HandlePauseProcess(processType);
        isProcessing = false;
    }

    public void OnModuleProcessComplete(KitchenItem kitchenItem)
    {
        kitchenItem.OnItemProcessComplete -= OnModuleProcessComplete;
        isProcessing = false;
    }
}
