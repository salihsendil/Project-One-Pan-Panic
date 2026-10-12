using UnityEngine;

public abstract class ContainerItemSO : ScriptableObject
{
    [Header("Base Settings")]
    public ItemType ItemType;
    public GameObject Prefab;
    public int Capacity;
}