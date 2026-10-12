using UnityEngine;


// This is the main class of the Plate Counter.
// It manages spawning plates and accepts plates from the player.
// The counter only accepts a plate from the player if it does not already have a plate.


[RequireComponent(typeof(ItemInteractionModule))]
[RequireComponent(typeof(PlateDispenserModule))]
public class PlateDispenserCounter : BaseCounter
{
    //References
    private IInteractor currentInteractor;
    private AdvancedPoolManager poolManager;

    [Header("Plate")]
    [SerializeField] protected PlateSO plateSO;
    [SerializeField] protected float plateRespawnDelay = 2f;


    private void OnEnable()
    {
        currentInteractor = GetComponent<IInteractor>();

        foreach (var module in stateModules)
            module.Initialize(currentInteractor);
        //singalBus.Subscribe<OrderDeliveredSignal>(RespawnPlate);
    }

    private void OnDisable()
    {
        foreach (var module in stateModules)
            module.Dispose();
        //singalBus.Unsubscribe<OrderDeliveredSignal>(RespawnPlate);
    }

    private void Start()
    {
        while (true)
        {
            Plate plate = poolManager.Spawn<Plate>(plateSO.ItemType);
            if (plate == null) break;
            currentInteractor.SetItem(plate);
        }


        //for (int i = 0; i < 5; i++)
        //{
        //    GameObject go = Instantiate(prefab, transform.position, Quaternion.identity);
        //    Plate plate = go.GetComponent<Plate>();
        //    currentInteractor.SetItem(plate);
        //}
    }

    private void RespawnPlate()
    {
        Plate plate = poolManager.Spawn<Plate>(plateSO.ItemType);
        if (plate == null) return;
        currentInteractor.SetItem(plate);
    }

    public override void InteractInstant(IInteractor incomingInteractor)
    {
        if (!currentInteractor.HasItem)
        {
            if (!incomingInteractor.HasItem) return;
            if (!incomingInteractor.GetItem.Transform.TryGetComponent(out Plate _)) return;
        }

        base.InteractInstant(incomingInteractor);
    }
}
