using UnityEngine;

[RequireComponent(typeof(IngredientIconDisplay))]
public class IngredientItem : BaseKitchenItem, IPoolable
{
    [SerializeField] private ItemStage itemStage;
    [SerializeField] private IngredientItemSO ingredientData;
    [SerializeField] private IngredientIconDisplay iconDisplay;

    public ItemStage ItemStage => itemStage;
    public IngredientItemSO IngredientData => ingredientData;
    public UniversalPoolEntryType GetPoolType => ingredientData.PoolType;


    protected override void Awake()
    {
        base.Awake();
        iconDisplay = GetComponent<IngredientIconDisplay>();
        itemStage = ingredientData.InitialStage;
        iconDisplay.SetIcon(ingredientData.Icon);
        iconDisplay.Hide();
    }

    public void Spawn()
    {
        itemStage = ingredientData.InitialStage;

        if (itemStage == ItemStage.Instant) iconDisplay.Show();
    }

    public void Despawn()
    {
        iconDisplay.Hide();
        UpdateMesh(ingredientData.InitialMesh);
    }

    public void ItemProcessed(Mesh newMesh, ItemStage newStage)
    {
        iconDisplay.Show();
        itemStage = newStage;
        UpdateMesh(newMesh);
    }

    public void OnAddedToContainer()
    {
        iconDisplay.Hide();
    }
}