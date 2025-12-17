using UnityEngine;

public class CuttingModule : MonoBehaviour, IInteractableAlternateModule
{
    private bool isProcessing;
    public bool IsProcessing() => isProcessing;

    private ProcessType processType = ProcessType.Cut;

    public bool CanInteractAlternate(ItemBehaviourController controller)
    {
        if (controller.CanProcess(processType)) { return true; }
        return false;
    }

    public void InteractAlternate(ItemBehaviourController controller, PlayerController playerController)
    {
        if (controller.WorkStage == WorkStage.Paused)
        {
            controller.HandleResumeProcess(processType);
        }

        else
        {
            controller.HandleProcessStart(processType);
        }

        controller.OnItemBehaviourProcessComplete += OnModuleProcessComplete;
        isProcessing = true;
    }

    public void InteractPause(ItemBehaviourController controller)
    {
        controller.OnItemBehaviourProcessComplete -= OnModuleProcessComplete;
        controller.HandlePauseProcess(processType);
        isProcessing = false;
    }

    public void OnModuleProcessComplete(ItemBehaviourController controller)
    {
        controller.OnItemBehaviourProcessComplete -= OnModuleProcessComplete;
        isProcessing = false;
    }
}
