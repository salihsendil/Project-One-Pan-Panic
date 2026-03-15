using UnityEngine;

[RequireComponent(typeof(SkinnedMeshRenderer))]

public class BodyMaterialFitter : MonoBehaviour, IBodyPartFitter
{
    private const int BODY_COLOR_INDEX = 0;
    private const int FACE_INDEX = 1;

    [SerializeField] private BodyPartType bodyPart;
    [SerializeField] private SkinnedMeshRenderer skinnedMesh;

    public BodyPartType BodyPart => bodyPart;

    private void Awake()
    {
        TryGetComponent(out skinnedMesh);
    }

    public void Apply(CustomizationData data)
    {
        var mats = skinnedMesh.materials;

        if (data.BodyColorMaterial != null)
        {
            if (mats[BODY_COLOR_INDEX] != null)
            {
                mats[BODY_COLOR_INDEX] = data.BodyColorMaterial;
                skinnedMesh.sharedMaterials = mats;
            }
        }
        if (data.FaceMaterial != null)
        {
            if (mats[FACE_INDEX] != null)
            {
                mats[FACE_INDEX] = data.FaceMaterial;
                skinnedMesh.sharedMaterials = mats;
            }
        }

    }
}
