using UnityEngine;

public class OrderViewerModule : IInteractableModule
{
    public bool TryInteract(PlayerCarryingController player)
    {
        Debug.Log("open or close panel.");
        return true;
    }
}
