using UnityEngine;

public class CookingModule : MonoBehaviour, IInteractableModule
{
    public bool TryInteract(PlayerCarryingController player)
    {
        Debug.Log("cooking process is here");
        return true;
    }
}
