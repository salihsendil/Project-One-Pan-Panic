using UnityEngine;

public class CookingModule : MonoBehaviour, IInteractableAutoModule
{
    private bool isProcessing;

    private ProcessType[] processTypes = { ProcessType.Cook, ProcessType.Burn };

    public bool IsProcessing() => isProcessing;

    public bool CanInteractableAuto(KitchenItem kitchenItem)
    {
        return TryGetAvailableProcessType(kitchenItem, out ProcessType _);
    }

    public void InteractAuto(KitchenItem kitchenItem)
    {
        if (!TryGetAvailableProcessType(kitchenItem, out ProcessType process)) { return; }

        if (kitchenItem.WorkStage == WorkStage.Paused)
        {
            kitchenItem.HandleResumeProcess(process);
            isProcessing = true;
        }

        else if (kitchenItem.CanProcess(process))
        {
            kitchenItem.HandleProcessStart(process);
            isProcessing = true;
        }

        kitchenItem.OnItemProcessComplete += HandleProcessComplete;
    }

    public bool TryInteractPause(KitchenItem kitchenItem)
    {
        if (kitchenItem.WorkStage == WorkStage.Processing)
        {
            if (!TryGetAvailableProcessType(kitchenItem, out ProcessType process)) { return false; }

            kitchenItem.OnItemProcessComplete -= HandleProcessComplete;
            kitchenItem.HandlePauseProcess(process);
            isProcessing = false;
            return true;
        }

        return false;
    }

    private bool TryGetAvailableProcessType(KitchenItem kitchenItem, out ProcessType processType)
    {
        processType = ProcessType.None;
        foreach (var process in processTypes)
        {
            if (kitchenItem.CanProcess(process))
            {
                processType = process;
                Debug.Log(process);
                return true;
            }
        }
        return false;
    }

    private void HandleProcessComplete(KitchenItem kitchenItem)
    {
        kitchenItem.OnItemProcessComplete -= HandleProcessComplete;
        isProcessing = false;
        InteractAuto(kitchenItem);
    }
}
