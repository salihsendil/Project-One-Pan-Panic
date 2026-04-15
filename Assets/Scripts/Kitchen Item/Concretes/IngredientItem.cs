using UnityEngine;


public class IngredientItem : BaseKitchenItem, IPoolable
{
    //Stage
    [SerializeField] private ItemStage itemStage;

    //Data
    [SerializeField] private IngredientItemSO ingredientItemSO;

    public ItemStage ItemStage => itemStage;
    public IngredientItemSO GetItemData => ingredientItemSO;

    public void SetItemStage(ItemStage newStage)
    {
        itemStage = newStage;
    }

    public void HandleItemUIState()
    {
        if (itemStage != ItemStage.Raw)
        {
            iconDisplay.SetCanvasVisibility(true);
            iconDisplay.SetImage(ingredientItemSO.Icon);
        }
    }

    #region Object Pooling
    public UniversalPoolEntryType GetPoolType => ingredientItemSO.PoolType;

    public void OnSpawn()
    {
        SetItemStage(ingredientItemSO.InitialStage);
        UpdateMesh(ingredientItemSO.InitialMesh);
        HandleItemUIState();
        transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
    }

    public void OnDespawn()
    {
        iconDisplay.AllClear();
    }

    #endregion
}