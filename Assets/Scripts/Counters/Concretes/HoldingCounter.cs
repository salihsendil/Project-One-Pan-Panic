using UnityEngine;

public class HoldingCounter : BaseCounter
{
    [SerializeField] private IInteractableModule[] modules;
    private void Awake()
    {
        modules = GetComponents<IInteractableModule>();
    }
    
    public override void Interact(PlayerCarryingController player)
    {
        foreach (var module in modules)
        {
            if (module.TryInteract(player))
            {
                break;
            }
        }
    }
}
