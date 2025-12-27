using UnityEngine;

public abstract class KitchenItemSO : ScriptableObject
{
    public string KitchenItemName;
    public BaseKitchenItem Prefab;
    public Mesh InitialMesh;
}