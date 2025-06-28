using UnityEngine;

public class KitchenItem : MonoBehaviour
{
    [SerializeField] private KitchenItemType kitchenItemType;
    [SerializeField] private KitchenItemSO kitchenItemData;
    [SerializeField] private MeshFilter meshFilter;
    [SerializeField] public bool IsProcessed = false;

    public KitchenItemSO KitchenItemData { get => kitchenItemData; }

    void Start()
    {
        TryGetComponent(out meshFilter);
    }

    public void UpdateVisual(Mesh newMesh)
    {
        meshFilter.mesh = newMesh;
    }
}
