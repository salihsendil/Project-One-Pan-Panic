using UnityEngine;

public class PlateDispenserModule : MonoBehaviour, IInteractableModule
{
    public bool TryInteract(PlayerCarryingController player)
    {
        Debug.Log("you can take plate in here");
        return true;
    }
}
