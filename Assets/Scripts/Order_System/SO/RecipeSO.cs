using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New_RecipeSO", menuName = "Scriptable Objects/Order/New RecipeSO")]
public class RecipeSO : ScriptableObject
{
    [Header("UI")]
    public Sprite Icon;

    [Header("Time")]
    public float PreparationTime;

    [Header("Scores")]
    public int SuccessScore;
    public int PenaltyScore;
    
    [Header("Required Ingredients")]
    public List<RecipeIngredient> Ingredients = new();
}

[Serializable]
public struct RecipeIngredient
{
    public IngredientItemSO IngredientSO;
    public PreparationStage RequiredStage;

    public RecipeIngredient(IngredientItemSO ingredientItemData, PreparationStage requiredStage)
    {
        IngredientSO = ingredientItemData;
        RequiredStage = requiredStage;
    }
}
