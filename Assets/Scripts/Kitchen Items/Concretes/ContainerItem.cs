using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(ContainerIconDisplay))]
public class ContainerItem : BaseKitchenItem, IPoolable, IContainer
{
    //Zenject
    [Inject] private UniversalPoolManager poolManager;
    [Inject] private RecipeMatchEvaluator recipeMatcher;
    [Inject] private SignalBus signalBus;


    [SerializeField] private ContainerItemSO containerData;
    [SerializeField] private Transform holdPoint;
    [SerializeField] private ContainerIconDisplay iconDisplay;
    private RecipeSO currentRecipe;

    private int currentSize = 0;

    private List<IngredientItem> plateItems = new();
    private HashSet<IngredientEntry> plateEntries = new();

    public RecipeSO CurrentRecipe => currentRecipe;

    protected override void Awake()
    {
        base.Awake();

        iconDisplay = GetComponent<ContainerIconDisplay>();
        iconDisplay.Hide();
    }

    #region IPoolable
    public UniversalPoolEntryType GetPoolType => containerData.PoolType;

    public void Spawn()
    {
        iconDisplay.Hide();
        currentSize = 0;
    }

    public void Despawn()
    {
        foreach (var item in plateItems)
        {
            poolManager.Despawn(item);
        }

        plateEntries.Clear();
        plateItems.Clear();

        UpdateMesh(containerData.InitialMesh);
        signalBus.Fire(new ContainerItemDespawnSignal());
    }
    #endregion

    private void SetItem(IPickable pickable)
    {
        pickable.Transform.DOJump(holdPoint.position, 0.5f, 1, 0.2f)
        .SetEase(Ease.InOutQuad)
        .OnComplete(() =>
        {
            pickable.Transform.SetParent(holdPoint);
            pickable.Transform.position = holdPoint.position;
            pickable.Transform.localPosition = Vector3.zero;
        });
    }

    public bool CanAddItem(IPickable pickable, out IngredientItem ingredient)
    {
        ingredient = null;
        if (currentSize >= containerData.Capacity) return false;
        if (!pickable.GetGameObject.TryGetComponent(out ingredient)) return false;
        if (ingredient.ItemStage == ItemStage.Raw || ingredient.ItemStage == ItemStage.Burnt) return false;

        IngredientEntry entry = new IngredientEntry(ingredient.IngredientData, ingredient.ItemStage);
        return !plateEntries.Contains(entry);

    }

    public void AddItem(IngredientItem ingredient)
    {
        IngredientEntry entry = new IngredientEntry(ingredient.IngredientData, ingredient.ItemStage);
        plateEntries.Add(entry);
        plateItems.Add(ingredient);
        currentSize++;

        ingredient.OnAddedToContainer();
        iconDisplay.ShowIcon(ingredient.IngredientData.Icon);

        SetItem(ingredient);
        CheckRecipeMatch();
    }

    private void CheckRecipeMatch()
    {
        if (!recipeMatcher.TryRecipeMatch(plateEntries, out currentRecipe)) return;

        foreach (var item in plateItems)
        {
            poolManager.Despawn(item);
        }

        plateItems.Clear();

        UpdateMesh(currentRecipe.Mesh);
    }
}
