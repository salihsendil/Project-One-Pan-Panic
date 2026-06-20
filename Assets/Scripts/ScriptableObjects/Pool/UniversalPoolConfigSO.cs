using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New UniversalPoolConfigSO", menuName = "Scriptable Objects/New UniversalPoolConfigSO")]
public class UniversalPoolConfigSO : ScriptableObject
{
    public List<UniversalPoolEntry> PoolEntries = new();
}

[Serializable]
public struct UniversalPoolEntry
{
    public ItemType Type;
    public GameObject Prefab;
    public int InitializeSize;
    public bool HasHardLimit;
}
