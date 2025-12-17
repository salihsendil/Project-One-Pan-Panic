using UnityEngine;

public class CookingModule : MonoBehaviour, IInteractableAutoModule
{
    private bool isProcessing;

    private ProcessType[] processTypes = { ProcessType.Cook, ProcessType.Burn };

    public bool IsProcessing() => isProcessing;

    public bool CanInteractableAuto(ItemBehaviourController controller)
    {
        return TryGetAvailableProcessType(controller, out ProcessType _);
    }

    public void InteractAuto(ItemBehaviourController controller)
    {
        if (!TryGetAvailableProcessType(controller, out ProcessType process)) { return; }

        if (controller.WorkStage == WorkStage.Paused)
        {
            controller.HandleResumeProcess(process);
            isProcessing = true;
        }

        else if (controller.CanProcess(process))
        {
            controller.HandleProcessStart(process);
            isProcessing = true;
        }

        controller.OnItemBehaviourProcessComplete += HandleProcessComplete;
    }

    public bool TryInteractPause(ItemBehaviourController controller)
    {
        if (controller.WorkStage == WorkStage.Processing)
        {
            if (!TryGetAvailableProcessType(controller, out ProcessType process)) { return false; }

            controller.OnItemBehaviourProcessComplete -= HandleProcessComplete;
            controller.HandlePauseProcess(process);
            isProcessing = false;
            return true;
        }

        return false;
    }

    private bool TryGetAvailableProcessType(ItemBehaviourController controller, out ProcessType processType)
    {
        processType = ProcessType.None;
        foreach (var process in processTypes)
        {
            if (controller.CanProcess(process))
            {
                processType = process;
                Debug.Log(process);
                return true;
            }
        }
        return false;
    }

    private void HandleProcessComplete(ItemBehaviourController controller)
    {
        controller.OnItemBehaviourProcessComplete -= HandleProcessComplete;
        isProcessing = false;
        InteractAuto(controller);
    }
}
