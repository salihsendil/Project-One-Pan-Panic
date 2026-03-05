using UnityEngine;

[RequireComponent(typeof(SkinnedMeshRenderer))]
public class BodyPartFitter : MonoBehaviour
{
    [SerializeField] private BodyPartType bodyPart;

    private Mesh currentMesh;
    private SkinnedMeshRenderer skinnedMesh;

    public BodyPartType BodyPart  => bodyPart;

    private void Awake()
    {
        if (skinnedMesh == null) { TryGetComponent(out skinnedMesh); }
        if (currentMesh == null) { currentMesh = skinnedMesh.sharedMesh; }
    }

    public void UpdateMesh(Mesh newMesh)
    {
        skinnedMesh.sharedMesh = newMesh;
    }
}
