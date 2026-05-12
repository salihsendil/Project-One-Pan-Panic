using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New OrderConfigSO", menuName = "Scriptable Objects/New OrderConfigSO")]
public class OrderConfigSO : ScriptableObject
{
    public List<RecipeEntry> RecipeEntries= new();
    public int MaxActiveOrderCount;
    public float StartOrderSpawnDelay;
    public float OrderSpawnDelay;

    [TextArea]
    [SerializeField] private string Description;
}

[Serializable]
public struct RecipeEntry
{
    public RecipeSO Recipe;
    public float StartWeight;
    public float EndWeight;
}
