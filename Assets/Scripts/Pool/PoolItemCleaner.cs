using System.Collections.Generic;

public static class PoolItemCleaner
{
    public static void RestoreAndReturn(IPoolable item, UniversalPoolManager poolManager)
    {
        item.OnDespawn();
        poolManager.Despawn(item.GetPoolType, item.GetGameObject);
    }

    public static void ClearContainerIngredients(List<IngredientItem> items, UniversalPoolManager poolManager)
    {
        foreach (var item in items)
        {
            item.OnDespawn();
            poolManager.Despawn(item.GetPoolType, item.GetGameObject);
        }
    }
}
