using UnityEngine;

public class KitchenItem : MonoBehaviour
{
    [SerializeField] private KitchenItemSO kitchenItemData;
    [SerializeField] private MeshFilter meshFilter;

    public KitchenItemSO KitchenItemData { get => kitchenItemData; }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TryGetComponent(out meshFilter);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateVisual(Mesh newMesh)
    {
        meshFilter.mesh = newMesh;
    }
}
