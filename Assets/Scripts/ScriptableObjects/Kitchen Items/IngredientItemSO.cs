using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New IngredientItemSO", menuName = "Scriptable Objects/New IngredientItemSO")]
public class IngredientItemSO : KitchenItemSO
{
    public IngredientID IngredientID;
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