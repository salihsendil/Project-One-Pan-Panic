using UnityEngine;

public abstract class BaseKitchenItem : MonoBehaviour
{
    private MeshFilter meshFilter;

    private void Start()
    {
        TryGetComponent(out meshFilter);
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
