using UnityEngine;

[RequireComponent(typeof(ItemCanvasBillboardHandler))]
[RequireComponent(typeof(ItemIconDisplay))]
public abstract class BaseKitchenItem : MonoBehaviour, IPickable
{
    //References
    protected MeshFilter meshFilter;
    protected ItemCanvasBillboardHandler billboardHandler;
    protected ItemIconDisplay iconDisplay;

    //Transfer
    private bool isPickable = true;

    public Transform Transform => transform;

    public GameObject GetGameObject => gameObject;

    public virtual bool IsPickable { get => isPickable; set => isPickable = value; }
    public ItemIconDisplay IconDisplay { get => iconDisplay; }

    protected virtual void Start()
    {
        if (meshFilter == null) meshFilter = GetComponentInChildren<MeshFilter>();

        billboardHandler = GetComponent<ItemCanvasBillboardHandler>();
        iconDisplay = GetComponent<ItemIconDisplay>();
    }

    public void UpdateMesh(Mesh newMesh)
    {
        if (meshFilter != null)
        {
            meshFilter.mesh = newMesh;
        }
    }
}