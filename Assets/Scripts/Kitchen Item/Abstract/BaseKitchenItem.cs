using UnityEngine;

public abstract class BaseKitchenItem : MonoBehaviour, IPoolable, IInfoProvider
{
    [SerializeField] private MeshFilter meshFilter;

    public abstract KitchenItemSO GetKitchenItemSO();

    public abstract UniversalPoolEntryType GetPoolType();

    public abstract void OnSpawn();

    public abstract void OnDespawn();

    public abstract void GetDataInfo();

    public GameObject GetGameObject() => gameObject;

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

    public virtual bool TryGetBehaviourController(out ItemBehaviourController controller)
    {
        controller = null;
        return false;
    }

    public virtual bool TryInteractWith(BaseKitchenItem kitchenItem)
    {
        return false;
    }
}