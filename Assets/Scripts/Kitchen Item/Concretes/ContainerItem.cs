using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ContainerItem : BaseKitchenItem
{
    //References
    [Inject] private OrderSystem orderSystem;
    [Inject] private RecipeMatchEvaluator recipeMatch;
    [Inject] private KitchenItemPoolManager poolManager;

    //Data
    [SerializeField] private ContainerItemSO containerItemSO;

    //HoldPoint
    [SerializeField] private Transform holdPoint;

    //Ingredient List
    [SerializeField] private List<IngredientItem> spawnedItems = new();
    [SerializeField] private List<IngredientEntry> ingredientEntries = new();

    //State
    private ContainerState containerState = ContainerState.Empty;

    //Recipe
    private RecipeSO currentRecipe;
    public RecipeSO CurrentRecipe => currentRecipe;


    public bool IsPlateReadyToServe() { return containerState == ContainerState.ReadyToServe; }

    public override KitchenItemSO GetKitchenItemSO() => containerItemSO;
    public List<IngredientItem> SpawnedItems => spawnedItems;
    public List<IngredientEntry> IngredientEntries => ingredientEntries;


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
        IngredientEntry newEntry = new IngredientEntry(((IngredientItemSO)ingredient.GetKitchenItemSO()).IngredientID, ingredient.ItemStage);

        if (!orderSystem.IsIngredientAllowedOnPlate(newEntry)) { return false; }

        spawnedItems.Add(ingredient);
        ingredientEntries.Add(newEntry);

        SetIngredientTransform(ingredient);

        CheckRecipeMatch();

        return true;
    }

    private void CheckRecipeMatch()
    {
        if (!recipeMatch.TryRecipeMatch(ingredientEntries, out currentRecipe))
        {
            containerState = ContainerState.Invalid;
            //UpdateMesh(); - will be update
            return;
        }

        KitchenItemRestorer.ClearContainerContents(spawnedItems, poolManager);
        containerState = ContainerState.ReadyToServe;
    }

    private void SetIngredientTransform(IngredientItem ingredient)
    {
        ingredient.transform.SetPositionAndRotation(holdPoint.position, holdPoint.transform.rotation);
        ingredient.transform.SetParent(holdPoint);
    }

    public override void RestoreItem()
    {
        transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        UpdateMesh(containerItemSO.InitialMesh);
        currentRecipe = null;
        containerState = ContainerState.Empty;
        spawnedItems.Clear();
        ingredientEntries.Clear();
    }
}
