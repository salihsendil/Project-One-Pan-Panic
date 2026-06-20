using UnityEngine;

[CreateAssetMenu(fileName = "New ContainerItemSO", menuName = "Scriptable Objects/Kitchen Items/New ContainerItemSO")]
public class ContainerItemSO : ScriptableObject
{
    [Header("Settings")]
    public string KitchenItemName;
    public int Capacity;

    [Header("Pool")]
    public ItemType ItemType;

    [Header("Visual")]
    public GameObject Prefab;
    public Mesh InitialMesh;
}
