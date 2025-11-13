using UnityEngine;

[RequireComponent(typeof(ItemHolderModule))]
public abstract class BaseCounter : MonoBehaviour
{
    public abstract void Interact(PlayerCarryingController player);
}
