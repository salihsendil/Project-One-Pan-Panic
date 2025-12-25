using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ContainerItem : BaseKitchenItem
{
    //References
    [Inject] private OrderSystem orderSystem;
    [Inject] private RecipeMatchEvaluator recipeMatch;

    //Ingredient List
    [SerializeField] private List<IngredientEntry> ingredientsOnPlate = new();

    //HoldPoint
    [SerializeField] private Transform holdPoint;

    //State
    private ContainerState containerState = ContainerState.Empty;

    //Recipe
    private string currentRecipeID;
    public string CurrentRecipeID  => currentRecipeID;


    public bool IsPlateReadyToServe() { return containerState == ContainerState.ReadyToServe; }

    public override bool TryInteractWith(BaseKitchenItem kitchenItem)
    {
        if (kitchenItem is IngredientItem ingredientItem)
        {
            return TryAddIngredient(ingredientItem);
        }
        return false;
    }

    public bool TryAddIngredient(IngredientItem ingredient)
    {
        IngredientEntry newEntry = new IngredientEntry(ingredient.KitchenItemSO.IngredientID, ingredient.ItemStage);

        if (!orderSystem.IsIngredientAllowedOnPlate(newEntry)) { return false; }

        ingredientsOnPlate.Add(newEntry);

        SetIngredientTransform(ingredient);

        CheckRecipeMatch();

        return true;
    }

    private void CheckRecipeMatch()
    {
        if (!recipeMatch.TryRecipeMatch(ingredientsOnPlate, out currentRecipeID))
        {
            containerState = ContainerState.InProgress;
            //UpdateMesh(); - will be update
            return;
        }

        containerState = ContainerState.ReadyToServe;
    }

    private void SetIngredientTransform(IngredientItem ingredient)
    {
        ingredient.transform.SetPositionAndRotation(holdPoint.position, holdPoint.transform.rotation);
        ingredient.transform.SetParent(holdPoint);
    }

    private void ClearPlate()
    {
        ingredientsOnPlate.Clear();
    }



    private List<int> testList = new List<int>();
    private Stack<int> testStack = new Stack<int>();
    private Queue<int> testQueue = new Queue<int>();



}
