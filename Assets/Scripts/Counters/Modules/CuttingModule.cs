using UnityEngine;

public class CuttingModule : MonoBehaviour, IInteractableModule
{
    public bool TryInteract(PlayerCarryingController player)
    {
        Debug.Log("cutting counter cut anything in here");
        return true;
    }
}
