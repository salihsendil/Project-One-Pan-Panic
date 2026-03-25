using UnityEngine;
using Zenject;

public class TrashModule : MonoBehaviour, IInstantModule
{
    [Inject] private UniversalPoolManager poolManager;

    public bool TryInteractionInstant(IInteractor interactor)
    {
        if (!interactor.HasItem) { return false; }
        if (!interactor.GetItem.GetGameObject.TryGetComponent(out IPoolable poolable)) { return false; }
        interactor.RemoveItem();
        PoolItemCleaner.RestoreAndReturn(poolable, poolManager);
        return true;
    }
}