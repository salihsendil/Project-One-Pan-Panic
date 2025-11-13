using UnityEngine;

[RequireComponent(typeof(ItemHolderModule))]
public abstract class BaseCounter : MonoBehaviour
{
    protected IInteractableModule[] counterModules = new IInteractableModule[2];

    protected virtual void Awake()
    {
        TryGetComponent(out ItemHolderModule itemHolderModule);
        counterModules[0] = itemHolderModule;
    }

    public abstract void Interact(PlayerCarryingController player);
}
