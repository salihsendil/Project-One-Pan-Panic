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

    public abstract void Interact(PlayerCarryingController player);
}
