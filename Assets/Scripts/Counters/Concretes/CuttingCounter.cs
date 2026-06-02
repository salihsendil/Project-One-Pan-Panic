using UnityEngine;

[RequireComponent(typeof(ItemInteractionModule))]
[RequireComponent(typeof(CuttingModule))]
public class CuttingCounter : BaseCounter
{
    //private IInteractor itemSocket;

    //protected override void Awake()
    //{
    //    base.Awake();
    //    itemSocket = GetComponent<IInteractor>();
    //}

    //public override void InteractionPerformed(IInteractor interactor)
    //{
    //    if (itemSocket.HasItem && interactor.HasItem) return;

    //    if (!itemSocket.HasItem && interactor.HasItem)
    //    {
    //        base.InteractionStarted(interactor);
    //    }

    //    foreach (var module in holdModules)
    //    {
    //        Debug.Log("deniyom");
    //        module?.OnInteractionPerformed();
    //    }
    //}
}
