using UnityEngine;

[RequireComponent(typeof(ItemInteractionModule))]
[RequireComponent(typeof(CuttingModule))]
[RequireComponent(typeof(CuttingCounterAnimationsController))]
public class CuttingCounter : BaseCounter, IInteractableAlternate
{
    private PlayerController currentPlayer;
    private CuttingCounterAnimationsController animationsController;
    private IInteractableAlternateModule[] alternateModules = new IInteractableAlternateModule[2];

    protected override void Awake()
    {
        base.Awake();

        TryGetComponent(out animationsController);

        TryGetComponent(out ItemInteractionModule itemInteractionModule);
        counterModules[0] = itemInteractionModule;

        TryGetComponent(out CuttingModule cuttingModule);
        alternateModules[0] = cuttingModule;
    }

    public override void Interact(PlayerCarryingController player)
    {
        foreach (var module in alternateModules)
        {
            if (module != null && module.IsProcessing())
            {
                return;
            }
        }

        base.Interact(player);
    }

    public void InteractAlternate(PlayerCarryingController playerCarrying, PlayerController playerController)
    {
        if (!itemSocket.HasItem() || playerCarrying.HasItem()) { return; }

        var kitchenItem = itemSocket.GetItem();

        foreach (var module in alternateModules)
        {
            if (module == null) { continue; }

            if (module.IsProcessing())
            {
                kitchenItem.OnItemProcessComplete -= HandleProcessComplete;
                currentPlayer = playerController;
                module.InteractPause(kitchenItem);
                HandleProcessState(false);
                break;
            }

            else if (module.CanInteractAlternate(kitchenItem))
            {

                kitchenItem.OnItemProcessComplete += HandleProcessComplete;
                module.InteractAlternate(kitchenItem, playerController);
                currentPlayer = playerController;
                HandleProcessState(true);
                break;

            }
        }
    }

    private void HandleProcessState(bool isProcessing)
    {
        animationsController.UpdateAnimationState(isProcessing);
        currentPlayer.SetBusyState(isProcessing);
    }

    private void HandleProcessComplete(KitchenItem kitchenItem)
    {
        kitchenItem.OnItemProcessComplete -= HandleProcessComplete;
        HandleProcessState(false);
    }
}
