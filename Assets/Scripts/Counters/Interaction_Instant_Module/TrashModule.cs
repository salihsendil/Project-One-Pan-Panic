using UnityEngine;
using Zenject;


// If the incoming item is an ingredient, it will be returned to the pool.
// If the incoming item is a container, the ingredients inside the container will be returned to the pool.
// Containers cannot be thrown into the Trash Module.


public class TrashModule : MonoBehaviour, IInstantInteractionModule
{
    [Inject] private AdvancedPoolManager poolManager;

    public bool TryInteractInstant(IInteractor incomingInteractor)
    {
        if (!incomingInteractor.HasItem) return false;
        if (incomingInteractor.GetItem.Transform.TryGetComponent(out IngredientItem ingredient))
        {
            incomingInteractor.RemoveItem();
            poolManager.Despawn(ingredient);
            return true;
        }

        else if (incomingInteractor.GetItem.Transform.TryGetComponent(out ContainerItem container))
        {
            if (container.CanExportItems())
            {
                var items = container.ExportItems();

                foreach (var item in items)
                {
                    poolManager.Despawn(item.IngredientItem);
                }
            }
            return true;
        }

        return false;
    }
}
