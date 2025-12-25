using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New KitchenItemSO", menuName = "Scriptable Objects/New KitchenItemSO")]
public class KitchenItemSO : ScriptableObject
{
    public IngredientID IngredientID;
    public BaseKitchenItem Prefab;
    public Sprite Icon;
    public ItemStage InitialStage;
    public List<ProcessRule> ProcessRules = new();
}

[Serializable]
public class ProcessRule
{
    public ProcessType ProcessType;
    public ItemStage FromStage;
    public ItemStage ToStage;
    public float ProcessTime;
    public Mesh OutputMesh;
}

