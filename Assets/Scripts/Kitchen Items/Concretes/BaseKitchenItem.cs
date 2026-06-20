using UnityEngine;

[RequireComponent(typeof(ItemCanvasBillboard))]
public abstract class BaseKitchenItem : MonoBehaviour, IPickable
{
    [SerializeField] private MeshFilter meshFilter;

    [SerializeField] protected bool isPickable = true;


    #region IPickable
    
    public Transform Transform => transform;

    public GameObject GetGameObject => gameObject;

    public abstract ItemType GetItemType();

    public bool IsPickable { get => isPickable; set => isPickable = value; }

    #endregion


    protected virtual void Awake()
    {
        if (meshFilter == null)
            meshFilter = GetComponentInChildren<MeshFilter>();
    }

    protected void UpdateMesh(Mesh newMesh)
    {
        if (meshFilter == null) return;
            meshFilter.sharedMesh = newMesh;
    }

}
