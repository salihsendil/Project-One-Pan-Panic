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

        if (!hasItem && !playerHasItem) { return false; }

        if (hasItem && item.WorkStage == WorkStage.Processing) { return false; }

        if (!hasItem)
        {
            if (playerHasItem) { itemSocket.SetItem(player.RemoveItem()); return true; }
        }

        else if (hasItem)
        {
            if (!player.HasItem()) { player.SetItem(itemSocket.RemoveItem()); return true; }

            else { Debug.Log("Player has plate!"); return true; }
        }

        return false;
    }
}
