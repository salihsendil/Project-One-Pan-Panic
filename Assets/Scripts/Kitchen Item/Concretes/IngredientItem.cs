using UnityEngine;

public class IngredientItem : BaseKitchenItem, IPoolable
{
    //Stage
    [SerializeField] private ItemStage itemStage;

    //Data
    [SerializeField] private IngredientItemSO ingredientItemSO;


    public ItemStage ItemStage => itemStage;
    public override KitchenItemSO GetKitchenItemSO() => ingredientItemSO;

    public void SetItemStage(ItemStage newStage)
    {
        itemStage = newStage;
    }

    #region Object Pooling
    public UniversalPoolEntryType GetPoolType => ingredientItemSO.Type;

    public void OnSpawn()
    {
        SetItemStage(ingredientItemSO.InitialStage);
        UpdateMesh(ingredientItemSO.InitialMesh);
        transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
    }

    public void OnDespawn() { }

    #endregion
}