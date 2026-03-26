using UnityEngine;
using Zenject;

public class PlateItem : BaseKitchenItem, IContainer
{
    //References
    [Inject] private OrderSystem orderSystem;




    [SerializeField] private ContainerItemSO containerItemSO;

    public bool AddIngredient(IngredientItem ingredient)
    {
        IngredientItemSO ingredientData = ingredient.GetItemData();

        Debug.Log($"TryAddIngredient called for {ingredientData.name} (stage: {ingredient.ItemStage})", this);

        IngredientEntry newEntry = new IngredientEntry(ingredientData, ingredient.ItemStage);

        if (!orderSystem.IsIngredientAllowedOnPlate(newEntry)) { return false; }

        //spawnedItems.Add(ingredient);

        //ingredientEntries.Add(newEntry);

        //SetIngredientTransform(ingredient);

        //CheckRecipeMatch();

        return true;
    }

    public bool CanAddIngredient(IngredientItem ingredient)
    {
        return true;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }


}
