using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New KitchenItemPoolConfigSO", menuName = "Scriptable Objects/New KitchenItemPoolConfigSO")]
public class KitchenItemPoolConfigSO : ScriptableObject
{
    public List<PoolItemEntry> KitchenItemEntries = new();
}


[Serializable]
public struct PoolItemEntry
{
    public KitchenItemSO KitchenItemSO;
    public int InitializeSize;
}
