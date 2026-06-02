using UnityEngine;
using Zenject;

public class DeliveryModule : MonoBehaviour, IInstantModule
{
    ////References
    //[Inject] private OrderManager orderManager;
    //[Inject] private UniversalPoolManager poolManager;

    public bool TryInteractionInstant(IInteractor interactor)
    {
        //    if (!interactor.HasItem) return false;

        //    if (!interactor.GetItem.GetGameObject.TryGetComponent(out ContainerItem containerItem)) return false;
        //    if (containerItem.CurrentRecipe == null) return false;

        //    if (orderManager.TryCompleteOrder(containerItem.CurrentRecipe))
        //    {
        //        interactor.RemoveItem();
        //        poolManager.Despawn(containerItem);
        //        return true;
        //    }

        //    Debug.LogError("It's not ordered!");

        return false;
    }
}
