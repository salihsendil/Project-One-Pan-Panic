using System.Collections.Generic;

public static class KitchenItemRestorer
{
    public static void RestoreAndReturn(BaseKitchenItem kitchenItem, KitchenItemPoolManager poolManager)
    {
        if (kitchenItem is ContainerItem containerItem)
        {
            FullRestoreContainer(containerItem, poolManager);
            return;
        }

        kitchenItem.RestoreItem();
        poolManager.ReturnItemBackToPool(kitchenItem.GetKitchenItemSO(), kitchenItem);
    }

    public static void ClearContainerContents(List<IngredientItem> ingredients, KitchenItemPoolManager poolManager)
    {
        foreach (var ingredient in ingredients)
        {
            ingredient.RestoreItem();
            poolManager.ReturnItemBackToPool(ingredient.GetKitchenItemSO(), ingredient);
        }

        ingredients.Clear();
    }

    public static void FullRestoreContainer(ContainerItem container, KitchenItemPoolManager poolManager)
    {
        ClearContainerContents(container.SpawnedItems, poolManager);
        container.RestoreItem();
        poolManager.ReturnItemBackToPool(container.GetKitchenItemSO(), container);
    }
}
