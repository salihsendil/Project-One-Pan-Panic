using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ContainerBehaviour : MonoBehaviour, IContainerItem
{
    [SerializeField] private List<KitchenItem> onPlateList = new List<KitchenItem>();
    [SerializeField] private HashSet<RecipeSO.RecipeIngredient> kitchemItemsDatas = new HashSet<RecipeSO.RecipeIngredient>();

    public HashSet<RecipeSO.RecipeIngredient> KitchemItemsDatas => kitchemItemsDatas;

    public bool CanPuttableOnPlate(KitchenItem kitchenItem)
    {
        return kitchenItem.IsProcessed && !onPlateList.Any(x => x.KitchenItemData == kitchenItem.KitchenItemData);
    }

    public void PutOnPlate(KitchenItem kitchenItem, IKitchenItemStateProvider stateProvider)
    {

        kitchenItem.transform.position = transform.position + Vector3.up / 10;
        kitchenItem.transform.SetParent(transform);
        onPlateList.Add(kitchenItem);
        kitchemItemsDatas.Add(new RecipeSO.RecipeIngredient(kitchenItem.KitchenItemData, stateProvider.CurrentState));
    }
}
