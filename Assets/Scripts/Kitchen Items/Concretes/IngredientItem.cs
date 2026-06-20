using UnityEngine;

[RequireComponent(typeof(IngredientIconDisplay))]
public class IngredientItem : BaseKitchenItem, IPoolable
{
    [SerializeField] private ItemStage itemStage;
    [SerializeField] private IngredientItemSO ingredientData;
    [SerializeField] private IngredientIconDisplay iconDisplay;

    public ItemStage ItemStage => itemStage;
    public IngredientItemSO IngredientData => ingredientData;
    public ItemType GetPoolType => ingredientData.ItemType;
    public override ItemType GetItemType() => ingredientData.ItemType;


    protected override void Awake()
    {
        base.Awake();
        iconDisplay = GetComponent<IngredientIconDisplay>();
        itemStage = ingredientData.InitialStage;
        iconDisplay.SetIcon(ingredientData.Icon);
        iconDisplay.Hide();
    }

    #region IPoolable

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

    #endregion

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