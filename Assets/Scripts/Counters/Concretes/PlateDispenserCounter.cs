using System.Collections;
using UnityEngine;
using Zenject;


// This is the main class of the Plate Counter.
// It manages spawning plates and accepts plates from the player.
// The counter only accepts a plate from the player if it does not already have a plate.


[RequireComponent(typeof(ItemInteractionModule))]
[RequireComponent(typeof(PlateDispenserModule))]
public class PlateDispenserCounter : BaseCounter
{
    //References
    [Inject] private AdvancedPoolManager poolManager;
    private IInteractor currentInteractor;

    //Coroutine
    private Coroutine spawnCoroutine;
    private WaitForSeconds spawnDelay;

    [Header("Plate")]
    [SerializeField] protected PlateSO plateSO;

    [Header("Respawn Plate")]
    [SerializeField] protected float plateSpawnInterval = 6f;


    private void OnEnable()
    {
        currentInteractor = GetComponent<IInteractor>();

        foreach (var module in stateModules)
            module.Initialize(currentInteractor);

        spawnDelay = new WaitForSeconds(plateSpawnInterval);
    }

    private void OnDisable()
    {
        foreach (var module in stateModules)
            module.Dispose();
        
        StopCoroutine(spawnCoroutine);
    }

    private void Start()
    {
        while (true)
        {
            Plate plate = poolManager.Spawn<Plate>(plateSO.ItemType);
            if (plate == null) break;
            currentInteractor.SetItem(plate);
        }

        spawnCoroutine = StartCoroutine(TrySpawnPlateFromPool());

        //for (int i = 0; i < 5; i++)
        //{
        //    GameObject go = Instantiate(prefab, transform.position, Quaternion.identity);
        //    Plate plate = go.GetComponent<Plate>();
        //    currentInteractor.SetItem(plate);
        //}
    }

    private IEnumerator TrySpawnPlateFromPool()
    {
        while (true)
        {
            yield return spawnDelay;

            Plate plate = poolManager.Spawn<Plate>(plateSO.ItemType);
            if (plate != null)
            {
                currentInteractor.SetItem(plate);
            }
        }
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
