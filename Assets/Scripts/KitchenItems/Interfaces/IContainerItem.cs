using System.Collections.Generic;
using UnityEngine;

public interface IContainerItem
{
    public HashSet<RecipeSO.RecipeIngredient> KitchemItemsDatas { get ; }
    public bool CanPuttableOnPlate(KitchenItem kitchenItem);
    public void PutOnPlate(KitchenItem kitchenItem, IKitchenItemStateProvider stateProvider);
}
