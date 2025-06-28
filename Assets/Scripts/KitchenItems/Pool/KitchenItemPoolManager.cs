using System.Collections.Generic;
using UnityEngine;

public class KitchenItemPoolManager : MonoBehaviour
{
    [SerializeField] private List<KitchenItemPool> itemPool;

    private Dictionary<KitchenItemType, Stack<KitchenItem>> poolDictionary = new Dictionary<KitchenItemType, Stack<KitchenItem>>();

    public Dictionary<KitchenItemType, Stack<KitchenItem>> PoolDictionary => poolDictionary;

    private void Awake()
    {
        FillTheDictionary();
    }

    public KitchenItem GetKitchenItemFromPool(KitchenItemType type)
    {
        var kitchenItem = poolDictionary[type]?.Pop();
        kitchenItem.gameObject.SetActive(true);
        return kitchenItem;
    }

    public void ReturnKitchenItemToPool(KitchenItem item)
    {
        item.gameObject.SetActive(false);

        if (poolDictionary.TryGetValue(item.KitchenItemData.KitchenItemType, out var pool))
        {
            pool.Push(item);
        }

        else
        {
            Debug.LogError($"No pool found for item type: {item.KitchenItemData.KitchenItemType}");
        }

    }

    private void FillTheDictionary()
    {
        for (int i = 0; i < itemPool.Count; i++)
        {
            var stack = new Stack<KitchenItem>();
            for (int j = 0; j < itemPool[i].initalSize; j++)
            {
                var kitchenItem = Instantiate(itemPool[i].kitchenItemData.Prefab, transform.position, Quaternion.identity);
                kitchenItem.gameObject.SetActive(false);
                stack.Push(kitchenItem);
            }
            poolDictionary.Add(itemPool[i].kitchenItemType, stack);
        }
    }
}
