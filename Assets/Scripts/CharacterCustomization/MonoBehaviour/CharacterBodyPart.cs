using UnityEngine;

public class CharacterBodyPart : MonoBehaviour
{
    [SerializeField] private BodyPartType bodyPartType;
    [SerializeField] private SkinnedMeshRenderer meshRenderer;

    public BodyPartType BodyPartType { get => bodyPartType; }
    public SkinnedMeshRenderer MeshRenderer { get => meshRenderer; }

    private void Awake()
    {
        TryGetComponent(out meshRenderer);

        if (meshRenderer == null)
        {
            Debug.LogError($"Body part skinned mesh renderer not found!: {bodyPartType}");
        }
    }

    public void ApplyMesh(Mesh mesh)
    {
        if (meshRenderer != null && meshRenderer.sharedMesh != mesh)
        {
            meshRenderer.sharedMesh = mesh;
        }
    }

}
