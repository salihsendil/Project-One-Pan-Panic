using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New RecipeSO", menuName = "Scriptable Objects/New RecipeSO")]
public class RecipeSO : ScriptableObject
{
    public Sprite RecipeIcon;
    public string RecipeName;
    public string RecipeID;
    public Mesh Mesh;
    public int SuccessScore;
    public int PenaltyScore;
    public float PreperationTime;
    public List<IngredientEntry> Ingredients = new();
}

[Serializable]
public struct IngredientEntry
{
    public IngredientItemSO IngredientItemData;
    public ItemStage RequiredStage;

    public IngredientEntry(IngredientItemSO ýngredientItemData, ItemStage requiredStage)
    {
        IngredientItemData = ýngredientItemData;
        RequiredStage = requiredStage;
    }
}
