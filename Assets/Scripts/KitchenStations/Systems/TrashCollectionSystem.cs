using Zenject;

public class TrashCollectionSystem : KitchenStation
{
    [Inject] private KitchenItemRestorer itemRestorer;

    public override void Interact()
    {
        if (transferItemHandler == null) { return; }

        if (!transferItemHandler.HasKitchenItem) { return; }

        transferItemHandler.GiveKitchenItem(out currentKitchenItem);

        itemRestorer.RestoreKitchenItemByType(RemoveKitchenItem());
    }
}
