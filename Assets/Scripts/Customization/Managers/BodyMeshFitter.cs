using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class BodyMeshFitter : MonoBehaviour, IBodyPartFitter
{
    [SerializeField] private BodyPartType bodyPart;

    [SerializeField] private MeshFilter meshFilter;

    public BodyPartType BodyPart => bodyPart;

    private void Awake()
    {
        if (meshFilter == null)
        {
            TryGetComponent(out meshFilter);
        }
    }
    public void Apply(CustomizationData data)
    {
        if (meshFilter == null) { return; }
        meshFilter.mesh = data.Mesh;
    }
}
