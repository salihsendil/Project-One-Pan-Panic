using UnityEngine;

[RequireComponent(typeof(ItemInteractionModule))]
[RequireComponent(typeof(CuttingModule))]
[RequireComponent(typeof(CuttingCounterAnimationsController))]
public class CuttingCounter : BaseCounter, IInteractableAlternate
{
    private ItemSocket itemSocket;
    private PlayerController currentPlayer;
    private CuttingCounterAnimationsController animationsController;
    private IInteractableAlternateModule[] alternateModules = new IInteractableAlternateModule[2];

    private void Awake()
    {
        TryGetComponent(out itemSocket);

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

        if (!kitchenItem.TryGetBehaviourController(out ItemBehaviourController controller)) { return; }

        foreach (var module in alternateModules)
        {
            if (module == null) { continue; }

            if (module.IsProcessing())
            {
                controller.OnItemBehaviourProcessComplete -= HandleProcessComplete;
                currentPlayer = playerController;
                module.InteractPause(controller);
                HandleProcessState(false);
                break;
            }

            else if (module.CanInteractAlternate(controller))
            {

                controller.OnItemBehaviourProcessComplete += HandleProcessComplete;
                module.InteractAlternate(controller, playerController);
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

    private void HandleProcessComplete(ItemBehaviourController controller)
    {
        controller.OnItemBehaviourProcessComplete -= HandleProcessComplete;
        HandleProcessState(false);
    }
}
