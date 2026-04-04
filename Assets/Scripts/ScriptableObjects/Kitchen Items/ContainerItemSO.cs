using UnityEngine;

[CreateAssetMenu(fileName = "New ContainerItemSO", menuName = "Scriptable Objects/New ContainerItemSO")]
public class ContainerItemSO : ScriptableObject
{
    [Header("ID")]
    public string KitchenItemName;
    
    [Header("Pool")]
    public UniversalPoolEntryType PoolType;

    [Header("Visual")]
    public GameObject Prefab;
    public Mesh InitialMesh;
}
