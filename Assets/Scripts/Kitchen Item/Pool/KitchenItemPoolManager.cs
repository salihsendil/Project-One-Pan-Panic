using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class KitchenItemPoolManager : MonoBehaviour
{
    //Zenject
    [Inject] private IInstantiator instantiator;

    //Data
    [SerializeField] private KitchenItemPoolConfigSO configSO;

    //Dictionaries
    private Dictionary<KitchenItemSO, PoolItemEntry> poolItemEntries = new();
    private Dictionary<KitchenItemSO, Stack<BaseKitchenItem>> kitchenItemPool = new();

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

            poolItemEntries[itemData] = poolEntry;
            Stack<BaseKitchenItem> itemQueue = new();
            kitchenItemPool[itemData] = itemQueue;

            for (int i = 0; i < poolEntry.InitializeSize; i++)
            {
                BaseKitchenItem item = instantiator.InstantiatePrefabForComponent<BaseKitchenItem>
                                      (itemData.Prefab, transform.position, Quaternion.identity, transform);
                item.gameObject.SetActive(false);
                itemQueue.Push(item);
            }
        }
    }

    public BaseKitchenItem GetItemFromPool(KitchenItemSO itemData)
    {
        if (!kitchenItemPool.TryGetValue(itemData, out var stack))
        {
            stack = new Stack<BaseKitchenItem>();
            kitchenItemPool[itemData] = stack;
        }

        if (!stack.TryPop(out BaseKitchenItem item))
        {
            if (poolItemEntries[itemData].HasHardLimit) { return null; }

            BaseKitchenItem obj = Instantiate(itemData.Prefab, transform.position, Quaternion.identity, transform);
            item = obj.GetComponent<BaseKitchenItem>();
        }
        item.gameObject.SetActive(true);
        return item;
    }

    public void ReturnItemBackToPool(KitchenItemSO itemData, BaseKitchenItem item)
    {
        if (!kitchenItemPool.ContainsKey(itemData)) { kitchenItemPool.Add(itemData, new Stack<BaseKitchenItem>()); }

        if (kitchenItemPool[itemData] == null) { kitchenItemPool[itemData] = new Stack<BaseKitchenItem>(); }

        item.gameObject.SetActive(false);
        item.transform.SetParent(transform);
        kitchenItemPool[itemData].Push(item);
    }
}
