using UnityEngine;

[CreateAssetMenu(fileName = "New_Process_SO", menuName = "Scriptable Objects/Kitchen Items/Ingredients/New Process SO")]
public class ProcessSO : ScriptableObject
{
    [Header("Process Settings")]
    public ProcessType ProcessType;

    [Header("Stage")]
    public VisualChangeTiming VisualChangeTiming;
    public Mesh OutputMesh;
    public Material OutputMaterial;

    [Header("Stage")]
    public PreparationStage FromStage;
    public PreparationStage ToStage;
}

public enum VisualChangeTiming
{
    None,
    OnProcessStart,
    OnProcessComplete
}
