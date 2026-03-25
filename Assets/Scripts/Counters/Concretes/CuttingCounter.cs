using UnityEngine;

[RequireComponent(typeof(ItemSocket))]
[RequireComponent(typeof(CuttingModule))]
[RequireComponent(typeof(CuttingCounterAnimationsController))]
public class CuttingCounter : BaseCounter
{
    private ItemSocket itemSocket;
    private CuttingModule cuttingModule;
    private CuttingCounterAnimationsController animationsController;

    protected override void Awake()
    {
        itemSocket = GetComponent<ItemSocket>();
        cuttingModule = GetComponent<CuttingModule>();
        base.Awake();
    }

    public override void InteractionStarted(IInteractor interactor)
    {
        foreach (var module in instantModules)
        {
            module?.TryInteractionInstant(interactor);
        }
    }

    public override void InteractionPerformed(IInteractor interactor)
    {
        foreach (var module in holdModules)
        {
            module?.OnInteractionStarted();
            module?.OnInteractionPerformed();
        }
    }


    //private void HandleProcessState(bool isProcessing)
    //{
    //    animationsController.UpdateAnimationState(isProcessing);
    //    currentPlayer.SetBusyState(isProcessing);
    //}

    //private void HandleProcessComplete(ItemBehaviourController controller)
    //{
    //    controller.OnItemBehaviourProcessComplete -= HandleProcessComplete;
    //    HandleProcessState(false);
    //}
}
