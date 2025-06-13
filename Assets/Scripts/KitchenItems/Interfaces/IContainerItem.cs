using System.Collections.Generic;
using UnityEngine;

public interface IContainerItem
{
    public List<KitchenItemSO> KitchemItemsDatas { get ; }
    public bool CanPuttableOnPlate(KitchenItem kitchenItem);
    public void PutOnPlate(KitchenItem kitchenItem);
}
