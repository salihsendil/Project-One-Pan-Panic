using UnityEngine;


public class TrashModule : MonoBehaviour, IInteractableModule
{
    public bool TryInteract(PlayerCarryingController player)
    {
        Debug.Log("throwing trahes in here");
        return true;
    }
}
