using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ContainerItem : BaseKitchenItem
{
    //References
    [Inject] private OrderSystem orderSystem;
    [Inject] private RecipeMatchEvaluator recipeMatch;
    [Inject] private UniversalPoolManager poolManager;
    [Inject] private SignalBus signalBus;

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

    //Pool Type
    public override UniversalPoolEntryType GetPoolType() => containerItemSO.Type;

    public bool IsPlateReadyToServe() { return containerState == ContainerState.ReadyToServe; }

    public override KitchenItemSO GetKitchenItemSO() => containerItemSO;

    public List<IngredientItem> SpawnedItems => spawnedItems;


    public override void OnSpawn()
    {
        containerState = ContainerState.Empty;
        UpdateMesh(containerItemSO.InitialMesh);
        transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
    }

    public override void OnDespawn()
    {
        currentRecipe = null;
        PoolItemCleaner.ClearContainerIngredients(spawnedItems, poolManager);
        spawnedItems.Clear();
        ingredientEntries.Clear();
        signalBus.Fire(new ContainerItemDespawned(GetPoolType()));
    }

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
        IngredientItemSO ingredientData = (IngredientItemSO)ingredient.GetKitchenItemSO();

        Debug.Log($"TryAddIngredient called for {ingredientData.name} (stage: {ingredient.ItemStage})", this);

        IngredientEntry newEntry = new IngredientEntry(ingredientData, ingredient.ItemStage);

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
            return;
        }

        PoolItemCleaner.ClearContainerIngredients(spawnedItems, poolManager);
        spawnedItems.Clear();
        //UpdateMesh(); - will be update
        containerState = ContainerState.ReadyToServe;
    }

    private void SetIngredientTransform(IngredientItem ingredient)
    {
        ingredient.transform.SetPositionAndRotation(holdPoint.position, holdPoint.transform.rotation);
        ingredient.transform.SetParent(holdPoint);
    }
}