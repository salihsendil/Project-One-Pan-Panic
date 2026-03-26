using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New IngredientItemSO", menuName = "Scriptable Objects/New IngredientItemSO")]
public class IngredientItemSO : ScriptableObject
{
    [Header("Name")]
    public string Name;
    
    [Header("Initial State")]
    public Mesh InitialMesh;
    public ItemStage InitialStage;


    [Header("Visual")]
    public Sprite Icon;
    public GameObject Prefab;


    [Header("Pool")]
    public UniversalPoolEntryType PoolType;

    [Header("Process Rules")]
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