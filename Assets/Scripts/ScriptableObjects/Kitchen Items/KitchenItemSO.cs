using UnityEngine;

public abstract class KitchenItemSO : ScriptableObject
{
    public string KitchenItemName;
    public UniversalPoolEntryType Type;
    public BaseKitchenItem Prefab;
    public Mesh InitialMesh;
}