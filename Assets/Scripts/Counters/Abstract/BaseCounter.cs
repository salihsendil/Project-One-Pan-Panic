using UnityEngine;

[RequireComponent(typeof(CounterHighlighter))]
public abstract class BaseCounter : MonoBehaviour, IInteractable<PlayerCarryingController>
{
    protected IInteractableModule[] counterModules = new IInteractableModule[2];

    public virtual void Interact(PlayerCarryingController player)
    {
        foreach (var module in counterModules)
        {
            if (module == null) { continue; }

            if (module.TryInteract(player))
            {
                break;
            }
        }
    }
}
