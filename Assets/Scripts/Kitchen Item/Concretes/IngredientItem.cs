using UnityEngine;

public class IngredientItem : BaseKitchenItem
{
    //References
    private ItemBehaviourController behaviourController;

    //Stage
    private ItemStage itemStage;
    public ItemStage ItemStage => itemStage;

    //Data
    [SerializeField] private IngredientItemSO ingredientItemSO;


    public override KitchenItemSO GetKitchenItemSO() => ingredientItemSO;

    private void Awake()
    {
        TryGetComponent(out behaviourController);
        itemStage = ingredientItemSO.InitialStage;
    }

    public override bool TryGetBehaviourController(out ItemBehaviourController controller)
    {
        controller = null;
        if (behaviourController == null) { return false; }

        controller = behaviourController;
        return true;
    }

    public void SetItemStage(ItemStage newStage)
    {
        itemStage = newStage;
    }

    public override void RestoreItem()
    {
        transform.SetParent(null);
        transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        SetItemStage(ingredientItemSO.InitialStage);
        UpdateMesh(ingredientItemSO.InitialMesh);
    }

}
