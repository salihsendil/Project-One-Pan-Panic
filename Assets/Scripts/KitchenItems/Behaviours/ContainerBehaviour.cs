using System.Collections.Generic;
using UnityEngine;

public class ContainerBehaviour : MonoBehaviour, IContainerItem
{
    [SerializeField] private List<KitchenItem> onPlateList = new List<KitchenItem>();
    [SerializeField] private List<KitchenItemSO> kitchemItemsDatas = new List<KitchenItemSO>();

    public List<KitchenItemSO> KitchemItemsDatas => kitchemItemsDatas;

    public bool CanPuttableOnPlate(KitchenItem kitchenItem)
    {
        return kitchenItem.isProcessed;
    }

    public void PutOnPlate(KitchenItem kitchenItem)
    {

        kitchenItem.transform.position = transform.position + Vector3.up / 10;
        kitchenItem.transform.SetParent(transform);
        onPlateList.Add(kitchenItem);
        kitchemItemsDatas.Add(kitchenItem.KitchenItemData);
    }
}
