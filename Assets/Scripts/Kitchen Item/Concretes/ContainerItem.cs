using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(ItemSocket))]
public class ContainerItem : BaseKitchenItem, IPoolable, IContainer
{
    [Inject] private OrderSystem orderSystem;
    [Inject] private RecipeMatchEvaluator recipeMatch;
    [Inject] private UniversalPoolManager poolManager;
    [Inject] private SignalBus signalBus;

    [SerializeField] private ContainerItemSO containerItemSO;
    [SerializeField] private IInteractor currentInteractor;

    [SerializeField] private List<IngredientItem> spawnedItems = new();
    [SerializeField] private List<IngredientEntry> ingredientEntries = new();

    private RecipeSO currentRecipe;

    public IInteractor GetInteractor => currentInteractor;
    public UniversalPoolEntryType GetPoolType => UniversalPoolEntryType.Plate;

    private void Awake()
    {
        currentInteractor = GetComponent<IInteractor>();
    }

    #region ObjectPooling

    public void OnSpawn()
    {
        UpdateMesh(containerItemSO.InitialMesh);
        transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
    }

    public void OnDespawn()
    {
        Debug.Log("despawn " + gameObject.name);
        currentRecipe = null;
        PoolItemCleaner.ClearContainerIngredients(spawnedItems, poolManager);
        spawnedItems.Clear();
        ingredientEntries.Clear();
        iconDisplay.AllClear();
        signalBus.Fire(new ContainerItemDespawned(GetPoolType));
    }

    #endregion

    public bool TryGetRecipe(out RecipeSO recipe)
    {
        recipe = currentRecipe;
        return recipe != null;
    }

    public bool CanInteractWith(IPickable pickable)
    {
        if (!pickable.IsPickable) return false;
        if (!pickable.GetGameObject.TryGetComponent(out IngredientItem ingredient)) return false;

        IngredientEntry entry = new IngredientEntry(ingredient.GetItemData, ingredient.ItemStage);
        if (!orderSystem.IsIngredientAllowedOnPlate(entry)) return false;

        return true;
    }

    public void InteractWith(IPickable pickable)
    {
        if (!pickable.IsPickable) return;
        if (!pickable.GetGameObject.TryGetComponent(out IngredientItem ingredient)) return;

        IngredientEntry entry = new IngredientEntry(ingredient.GetItemData, ingredient.ItemStage);

        currentInteractor.SetItem(pickable);
        spawnedItems.Add(ingredient);
        ingredientEntries.Add(entry);
        ingredient.IconDisplay.SetCanvasVisibility(false);
        iconDisplay.SetCanvasVisibility(true);
        iconDisplay.SetImage(ingredient.GetItemData.Icon);
        CheckRecipeMatch();
    }

    private void CheckRecipeMatch()
    {
        if (!recipeMatch.TryRecipeMatch(ingredientEntries, out currentRecipe)) return;

        PoolItemCleaner.ClearContainerIngredients(spawnedItems, poolManager);
        spawnedItems.Clear();
    }
}