using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New RecipeSO", menuName = "Scriptable Objects/New RecipeSO")]
public class RecipeSO : ScriptableObject
{
    public string RecipeName;
    public string RecipeID;
    public Sprite RecipeIcon;
    public Mesh Mesh;
    public int SuccessScore;
    public int PenaltyScore;
    public float PreparationTime;
    public List<IngredientEntry> Ingredients = new();
}

[Serializable]
public struct IngredientEntry
{
    public IngredientItemSO IngredientItemData;
    public ItemStage RequiredStage;

    public IngredientEntry(IngredientItemSO ingredientItemData, ItemStage requiredStage)
    {
        IngredientItemData = ingredientItemData;
        RequiredStage = requiredStage;
    }
}
