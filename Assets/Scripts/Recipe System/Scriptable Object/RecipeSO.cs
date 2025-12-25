using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New RecipeSO", menuName = "Scriptable Objects/New RecipeSO")]
public class RecipeSO : ScriptableObject
{
    public string RecipeName;
    public string RecipeID;
    public List<IngredientEntry> Ingredients = new();
    public int SuccessScore;
    public int PenaltyScore;
    public float PreperationTime;
}

[Serializable]
public struct IngredientEntry
{
    public IngredientID IngredientID;
    public ItemStage RequiredStage;

    public IngredientEntry(IngredientID ingredientID, ItemStage itemStage)
    {
        IngredientID = ingredientID;
        RequiredStage = itemStage;
    }
}
