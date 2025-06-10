using UnityEngine;

public class TrashCollectionSystem : KitchenStation
{
    public override void Interact()
    {
        if (transferItemHandler == null) { return; }

        transferItemHandler.GiveKitchenItem(out var kitchenItem);
        Destroy(kitchenItem.gameObject);
    }
}
