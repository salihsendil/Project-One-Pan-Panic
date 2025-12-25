using UnityEngine;

public class IngredientItem : BaseKitchenItem
{
    //References
    private ItemBehaviourController behaviourController;

    //Stage
    private ItemStage itemStage;
    public ItemStage ItemStage => itemStage;

    //Data
    [SerializeField] private KitchenItemSO kitchenItemSO;
    public KitchenItemSO KitchenItemSO => kitchenItemSO;


    private void Awake()
    {
        TryGetComponent(out behaviourController);
    }

    private void OnEnable()
    {
        itemStage = kitchenItemSO.InitialStage;
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
}
