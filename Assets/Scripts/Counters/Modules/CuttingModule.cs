using UnityEngine;

public class CuttingModule : MonoBehaviour, IInteractableAlternateModule
{
    private ProcessType processType = ProcessType.Cut;

    public bool CanInteractAlternate(KitchenItem kitchenItem)
    {
        if (kitchenItem.CanProcess(processType)) { return true; }
        return false;
    }

    public bool TryInteractAlternate(KitchenItem kitchenItem)
    {
        if (!CanInteractAlternate(kitchenItem)) { return false; }

        else
        {
            kitchenItem.StartProcess(processType);
        }

        return true;
    }
}
