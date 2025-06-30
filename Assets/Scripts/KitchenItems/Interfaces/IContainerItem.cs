using System.Collections.Generic;

public interface IContainerItem
{
    public List<KitchenItem> OnContainerList { get; }
    public HashSet<RecipeSO.RecipeIngredient> KitchemItemsDatas { get ; }
    public bool CanPuttableOnPlate(KitchenItem kitchenItem);
    public void PutOnPlate(KitchenItem kitchenItem, IKitchenItemStateProvider stateProvider);
}
