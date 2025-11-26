using UnityEngine;

[RequireComponent(typeof(ItemSocket))]
[RequireComponent(typeof(CounterHighlighter))]
public abstract class BaseCounter : MonoBehaviour, IInteractable<PlayerCarryingController>
{
    protected ItemSocket itemSocket;
    protected IInteractableModule[] counterModules = new IInteractableModule[2];

    protected virtual void Awake()
    {
        TryGetComponent(out itemSocket);
    }

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
