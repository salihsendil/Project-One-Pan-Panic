using UnityEngine;

public class IngredientItem : BaseKitchenItem
{
    //References
    private ItemBehaviourController behaviourController;

    //Stage
    private ItemStage itemStage;

    //Data
    [SerializeField] private IngredientItemSO ingredientItemSO;


    public ItemStage ItemStage => itemStage;
    public override UniversalPoolEntryType GetPoolType() => ingredientItemSO.Type;
    public override KitchenItemSO GetKitchenItemSO() => ingredientItemSO;

    private void Awake()
    {
        if (behaviourController == null) { TryGetComponent(out behaviourController); }
        SetItemStage(ingredientItemSO.InitialStage);
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

    public override void OnSpawn()
    {
        SetItemStage(ingredientItemSO.InitialStage);
        UpdateMesh(ingredientItemSO.InitialMesh);
        transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
    }

    public override void OnDespawn()
    {

    }
}