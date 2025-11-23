using UnityEngine;

public class CookingModule : MonoBehaviour, IInteractableAlternateModule
{
    private ProcessType processType = ProcessType.Cook;

    public bool CanInteractAlternate(KitchenItem kitchenItem)
    {
        if (kitchenItem.CanProcess(processType)) { return true; }
        return false;
    }

    public void InteractAlternate(KitchenItem kitchenItem)
    {
        kitchenItem.StartProcess(processType);
    }
}
