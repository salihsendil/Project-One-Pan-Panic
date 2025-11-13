using UnityEngine;

public class IngredientDispenserModule : MonoBehaviour, IInteractableModule
{
    public bool TryInteract(PlayerCarryingController player)
    {
        Debug.Log("ingredient dispenser module instantiate here");
        return true;
    }
}
