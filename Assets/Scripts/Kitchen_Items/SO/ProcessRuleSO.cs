using UnityEngine;

[CreateAssetMenu(fileName = "New ProcessRuleSO", menuName = "Scriptable Objects/Kitchen Items/New ProcessRuleSO")]
public class ProcessRuleSO : ScriptableObject
{
    [Header("Process Settings")]
    public ProcessType ProcessType;
    public float ProcessTime;
    
    [Header("Stages")]
    public ItemStage FromStage;
    public ItemStage ToStage;

    [Header("Output")]
    public Mesh OutputMesh;
}
