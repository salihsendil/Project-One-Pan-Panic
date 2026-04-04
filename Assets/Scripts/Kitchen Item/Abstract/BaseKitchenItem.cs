using UnityEngine;

[RequireComponent(typeof(IconBillboardHandler))]
public abstract class BaseKitchenItem : MonoBehaviour, IPickable
{
    //References
    protected MeshFilter meshFilter;
    protected IconBillboardHandler billboardHandler;

    //Transfer
    private bool isPickable = true;

    public Transform Transform => transform;

    public GameObject GetGameObject => gameObject;

    public virtual bool IsPickable { get => isPickable; set => isPickable = value; }
    public IconBillboardHandler BillboardHandler { get => billboardHandler; }

    protected virtual void Start()
    {
        if (meshFilter == null) meshFilter = GetComponentInChildren<MeshFilter>();
        billboardHandler = GetComponent<IconBillboardHandler>();
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