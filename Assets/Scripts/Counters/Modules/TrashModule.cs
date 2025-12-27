using UnityEngine;
using Zenject;

public class TrashModule : MonoBehaviour, IInteractableModule
{
    [Inject] private KitchenItemPoolManager poolManager;
    public bool TryInteract(PlayerCarryingController player)
    {
        KitchenItemRestorer.RestoreAndReturn(player.RemoveItem(), poolManager);
        return true;
    }
}
