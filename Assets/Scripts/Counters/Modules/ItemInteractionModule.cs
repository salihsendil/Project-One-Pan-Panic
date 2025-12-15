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
        var item = itemSocket.GetItem();
        bool hasItem = item != null;
        bool playerHasItem = player.HasItem();

        if (!hasItem)
        {
            if (!playerHasItem) { return false; }

            itemSocket.SetItem(player.RemoveItem());
            return true;
        }

        else if (hasItem)
        {
            if (!player.HasItem()) { player.SetItem(itemSocket.RemoveItem()); return true; }

            else { Debug.Log("Player has plate!"); return true; }
        }
        return false;
    }
}
