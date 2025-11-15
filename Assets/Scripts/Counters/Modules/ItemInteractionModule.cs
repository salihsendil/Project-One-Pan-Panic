using UnityEngine;

public class ItemInteractionModule : MonoBehaviour, IInteractableModule
{
    private ItemSocket itemSocket;

    private void Start()
    {
        TryGetComponent(out itemSocket);
    }

    public bool TryInteract(PlayerCarryingController player)
    {
        if (!itemSocket.HasItem() && !player.HasItem()) { return false; }

        else if (itemSocket.HasItem() && !player.HasItem()) { player.SetItem(itemSocket.RemoveItem()); return true; }

        else if (!itemSocket.HasItem() && player.HasItem()) { itemSocket.SetItem(player.RemoveItem()); return true; }

        else if (itemSocket.HasItem() && player.HasItem()) { Debug.Log("Player has plate!"); return true; }

        return false;
    }
}
