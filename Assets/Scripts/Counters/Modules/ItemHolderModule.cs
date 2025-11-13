using UnityEngine;

public class ItemHolderModule : MonoBehaviour, IInteractableModule
{
    [SerializeField] private Transform holdPoint;
    [SerializeField] private GameObject currentItem;

    public bool HasItem() => currentItem != null;
    public GameObject GetItem() { return currentItem; }
    public bool TryInteract(PlayerCarryingController player)
    {
        if (!HasItem() && !player.HasItem()) { return false; }

        else if (HasItem() && !player.HasItem()) { player.SetItem(RemoveItem()); }

        else if (!HasItem() && player.HasItem()) { SetItem(player.RemoveItem()); }

        else if (HasItem() && player.HasItem()) { Debug.Log("Player has plate!"); }

        return true;
    }

    private void SetItem(GameObject obj)
    {
        currentItem = obj;
        currentItem.transform.position = holdPoint.position;
        currentItem.transform.SetParent(holdPoint);
    }

    private GameObject RemoveItem()
    {
        var tempItem = currentItem;
        currentItem = null;
        return tempItem;
    }
}
