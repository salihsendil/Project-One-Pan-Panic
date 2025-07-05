using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ContainerDispenserSystem : KitchenStation
{
    [Inject] private KitchenItemPoolManager poolManager;
    [Inject] private BillboardManager billboardManager;
    private Stack<KitchenItem> plateStack = new Stack<KitchenItem>();
    [SerializeField] private Vector3 spawnOffset = new Vector3(0f, 0.05f, 0f);
    [SerializeField] private int plateStackInitSize;

    private void Start()
    {
        plateStackInitSize = poolManager.PoolDictionary[KitchenItemType.Plate].Count;
        InitPlates(plateStackInitSize);
    }

    private void InitPlates(int size)
    {
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

        else if (transferItemHandler.HasKitchenItem)
        {
            var kitchenItem = transferItemHandler.GetKitchenItem;

            if (!kitchenItem.TryGetComponent(out ContainerBehaviour container))
            {
                return;
            }

            transferItemHandler.GiveKitchenItem(out kitchenItem);

            if (kitchenItem.TryGetComponent(out ContainerBehaviour plate))
            {
                AddPlateToStack(kitchenItem);
                return;
            }

            if (currentKitchenItem.TryGetComponent(out ContainerBehaviour containerBehaviour) && containerBehaviour.CanPuttableOnPlate(kitchenItem))
            {
                if (kitchenItem.TryGetComponent(out IKitchenItemStateProvider stateProvider))
                {
                    containerBehaviour.PutOnPlate(kitchenItem, stateProvider);
                }
            }
        }
    }

    private void AddPlateToStack(KitchenItem item)
    {
        if (plateStack.Count >= plateStackInitSize)
        {
            Destroy(item.gameObject);
            return;
        }

        item.transform.position = kitchenItemPoint.position + spawnOffset * plateStack.Count;

        item.transform.SetParent(kitchenItemPoint.transform);

        plateStack.Push(item);

        currentKitchenItem = item;

        item.TryGetComponent(out ContainerIconBillboarding containerIcon);

        billboardManager.UnRegisterContainerToBillBoarding(containerIcon);
    }

    public KitchenItem GetPlateFromStack()
    {
        if (plateStack.Count == 0) { return null; }

        KitchenItem plate = plateStack.Pop();

        RemoveKitchenItem();

        plateStack.TryPeek(out currentKitchenItem);

        plate.TryGetComponent(out ContainerIconBillboarding containerIcon);

        billboardManager.RegisterContainerToBillBoarding(containerIcon);

        return plate;
    }

    public void ReturnPlateToStack(KitchenItem plate)
    {
        AddPlateToStack(plate);
    }
}
