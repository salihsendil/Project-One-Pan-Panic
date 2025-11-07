using UnityEngine;

public class CharacterPartHandler : MonoBehaviour
{
    [SerializeField] private CharacterPartType characterPart;
    [SerializeField] private SkinnedMeshRenderer skinnedMesh;

    public CharacterPartType CharacterPart { get => characterPart; }
    public SkinnedMeshRenderer SkinnedMesh { get => skinnedMesh; }

    void Awake()
    {
        if (skinnedMesh == null) { TryGetComponent(out skinnedMesh); }
    }

    public void ApplyMesh(Mesh newMesh)
    {
        if (skinnedMesh != null)
        {
            skinnedMesh.sharedMesh = newMesh;
        }
    }
}
