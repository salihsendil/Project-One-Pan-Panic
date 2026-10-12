using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Pool Config SO", menuName = "Scriptable Objects/Level/New Pool Config SO")]
public class PoolConfigSO : ScriptableObject
{
    public List<PoolEntry> PoolEntries = new();
}

[Serializable]
public struct PoolEntry
{
    public ItemType Type;
    public GameObject Prefab;
    public int Size;
    public bool HasHardLimit;
}
