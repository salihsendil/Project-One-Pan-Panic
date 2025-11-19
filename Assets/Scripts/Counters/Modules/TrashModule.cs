using UnityEngine;


public class TrashModule : MonoBehaviour, IInteractableModule
{
    public bool TryInteract(PlayerCarryingController player)
    {
        Debug.Log("throwing trashes in here");
        return true;
    }
}
