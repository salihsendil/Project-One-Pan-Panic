using UnityEngine;

public class CookingModule : MonoBehaviour, IInteractableAutoModule
{
    private ProcessType[] processTypes = { ProcessType.Cook, ProcessType.Burn };

    public bool CanInteractableAuto(KitchenItem kitchenItem)
    {
        foreach (var process in processTypes)
        {
            if (kitchenItem.CanProcess(process))
            {
                return true;
            }
        }
        return false;
    }

    public void TryInteractAuto(KitchenItem kitchenItem)
    {
        foreach (var process in processTypes)
        {
            if (kitchenItem.TryHandleProcess(process))
            {
                break;
            }
        }
    }
}
