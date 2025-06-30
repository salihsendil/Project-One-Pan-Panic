using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ContainerDispenserSystem : KitchenStation
{
    [Inject] private KitchenItemPoolManager poolManager;
    private Stack<KitchenItem> plateStack = new Stack<KitchenItem>();
    [SerializeField] private Vector3 spawnOffset = new Vector3(0f, 0.05f, 0f);

    private void Start()
    {
        InitPlates(poolManager.PoolDictionary[KitchenItemType.Plate].Count);
    }

    private void InitPlates(int size)
    {
        Debug.Log(poolManager.PoolDictionary[KitchenItemType.Plate].Count);

        for (int i = 0; i < size; i++)
        {
            KitchenItem plate = poolManager.GetKitchenItemFromPool(KitchenItemType.Plate);
            AddPlateToStack(plate);
        }
    }

    public override void Interact()
    {
        if (transferItemHandler == null) { return; }

        if (IsOccupied && !transferItemHandler.HasKitchenItem)
        {
            transferItemHandler.ReceiveKitchenItem(GetPlateFromStack());
        }

        else if (!IsOccupied && transferItemHandler.HasKitchenItem)
        {

        }
    }

    private void AddPlateToStack(KitchenItem item)
    {
        item.transform.position = kitchenItemPoint.position + spawnOffset * plateStack.Count;

        item.transform.SetParent(kitchenItemPoint.transform);

        plateStack.Push(item);

        currentKitchenItem = item;
    }

    public KitchenItem GetPlateFromStack()
    {
        if (plateStack.Count == 0) { return null; }

        KitchenItem plate = plateStack.Pop();

        RemoveKitchenItem();

        plateStack.TryPeek(out currentKitchenItem);

        return plate;
    }

    public void ReturnPlateToStack(KitchenItem plate)
    {
        AddPlateToStack(plate);
    }
}
