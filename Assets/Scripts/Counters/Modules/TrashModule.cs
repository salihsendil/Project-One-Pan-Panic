using UnityEngine;
using Zenject;

public class TrashModule : MonoBehaviour, IInteractableModule
{
    [Inject] private UniversalPoolManager poolManager;
    public bool TryInteract(PlayerCarryingController player)
    {
        PoolItemCleaner.RestoreAndReturn(player.RemoveItem(), poolManager);
        return true;
    }
}