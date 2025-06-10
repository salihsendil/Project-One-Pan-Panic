using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerDispenserSystem : KitchenStation
{
    private const int CONTAINER_POOL_SIZE = 5;

    [SerializeField] private GameObject prefab;
    private Stack<GameObject> plateStack = new Stack<GameObject>();
    [SerializeField] private Vector3 spawnOffset = new Vector3(0f, 0.05f, 0f);
    private Coroutine reSpawnCoroutine;
    [SerializeField] private float reSpawnDelay = 8f;


    private void Start()
    {

        for (int i = 0; i < CONTAINER_POOL_SIZE; i++)
        {
            FillTheStack(i);
        }

        StartCoroutine(ReSpawnPlate());
    }

    public override void Interact()
    {
        if (transferItemHandler == null) { return; }

        if (IsOccupied && !transferItemHandler.HasKitchenItem)
        {
            transferItemHandler.ReceiveKitchenItem(RemoveKitchenItem());
            plateStack.Pop();
            if (plateStack.Count > 0)
            {
                plateStack.Peek().TryGetComponent(out currentKitchenItem);
            }
            else
            {
                currentKitchenItem = null;
            }
        }

        else if (!IsOccupied && transferItemHandler.HasKitchenItem)
        {
            
        }
    }

    private IEnumerator ReSpawnPlate()
    {
        while (true)
        {
            yield return new WaitUntil(() => plateStack.Count < CONTAINER_POOL_SIZE);

            yield return new WaitForSeconds(reSpawnDelay);

            FillTheStack(plateStack.Count);
        }
    }

    private void FillTheStack(int spawnIndex)
    {
        GameObject container = Instantiate(prefab, kitchenItemPoint.position, Quaternion.identity);
        container.transform.position += spawnOffset * spawnIndex;
        container.transform.SetParent(kitchenItemPoint);
        plateStack.Push(container);
        plateStack.Peek().TryGetComponent(out currentKitchenItem);
    }
}
