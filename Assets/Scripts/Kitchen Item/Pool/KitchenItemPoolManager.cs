using System.Collections.Generic;
using UnityEngine;

public class KitchenItemPoolManager : MonoBehaviour
{
    [SerializeField] private KitchenItemPoolConfigSO configSO;

    private Dictionary<KitchenItemSO, Queue<BaseKitchenItem>> kitchenItemPool = new();

    private void Awake()
    {
        InitializePool();
    }

    private void InitializePool()
    {
        if (configSO == null) { return; }

        foreach (var poolEntry in configSO.KitchenItemEntries)
        {
            var itemData = poolEntry.KitchenItemSO;
            Queue<BaseKitchenItem> itemQueue = new();
            kitchenItemPool[poolEntry.KitchenItemSO] = itemQueue;

            for (int i = 0; i < poolEntry.InitializeSize; i++)
            {
                BaseKitchenItem item = Instantiate(itemData.Prefab, transform.position, Quaternion.identity, transform);
                item.gameObject.SetActive(false);
                itemQueue.Enqueue(item);
            }
        }
    }

    public BaseKitchenItem GetItemFromPool(KitchenItemSO itemData)
    {
        if (!kitchenItemPool.TryGetValue(itemData, out var queue))
        {
            kitchenItemPool[itemData] = new Queue<BaseKitchenItem>();
        }

        if (!queue.TryDequeue(out BaseKitchenItem item))
        {
            BaseKitchenItem obj = Instantiate(itemData.Prefab, transform.position, Quaternion.identity, transform);
            item = obj.GetComponent<BaseKitchenItem>();
        }
        item.gameObject.SetActive(true);
        return item;
    }

    public void ReturnItemBackToPool(KitchenItemSO itemData, BaseKitchenItem item)
    {
        if (kitchenItemPool[itemData] == null) { kitchenItemPool[itemData] = new Queue<BaseKitchenItem>(); }
        item.gameObject.SetActive(false);
        kitchenItemPool[itemData].Enqueue(item);
    }
}
