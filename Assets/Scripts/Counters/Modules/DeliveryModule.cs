using UnityEngine;

public class DeliveryModule : MonoBehaviour, IInteractableModule
{
    public bool TryInteract(PlayerCarryingController player)
    {
        Debug.Log("delivering order in here");
        return true;
    }
}
