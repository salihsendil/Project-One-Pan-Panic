using UnityEngine;

public abstract class BaseKitchenItem : MonoBehaviour, IPickable
{
    [SerializeField] private MeshFilter meshFilter;
    private bool canPickable = true;

    public Transform Transform => transform;

    public GameObject GetGameObject => gameObject;

    public virtual bool IsPickable { get => canPickable; set => canPickable = value; }

    public abstract KitchenItemSO GetKitchenItemSO();

    protected void Start()
    {
        if (meshFilter == null) { meshFilter = GetComponentInChildren<MeshFilter>(); }
    }

    public void UpdateMesh(Mesh newMesh)
    {
        if (meshFilter != null)
        {
            meshFilter.mesh = newMesh;
        }
    }

    public virtual bool TryInteractWith(BaseKitchenItem kitchenItem)
    {
        return false;
    }
}