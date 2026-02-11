using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderSystem : MonoBehaviour
{
    //Debug
    [SerializeField] private bool CanSpawnOrder = true;

    //Config
    [SerializeField] private OrderConfigSO orderConfig;
    public OrderConfigSO OrderConfig { get => orderConfig; }

    //Order List
    private List<RecipeSO> activeOrders = new();
    private List<RecipeSO> recipes => orderConfig.RecipeList;

    //Allowed Ingredients
    private HashSet<IngredientEntry> allowedIngredientSet = new HashSet<IngredientEntry>();


    private void Awake()
    {
        InitializeAllowedIngredientSet();
    }

    void Start()
    {
        StartCoroutine(TrySpawnOrderPeriodically());
    }

    #region Allowed Ingredient Set

    private void InitializeAllowedIngredientSet()
    {
        foreach (var recipe in recipes)
        {
            foreach (var entry in recipe.Ingredients)
            {
                allowedIngredientSet.Add(entry);
            }
        }
    }

    public bool IsIngredientAllowedOnPlate(IngredientEntry entry)
    {
        return allowedIngredientSet.Contains(entry);
    }

    #endregion

    IEnumerator TrySpawnOrderPeriodically()
    {
        while (CanSpawnOrder) //debug
        {
            if (orderConfig.MaxActiveOrderCount <= activeOrders.Count)
            {
                yield return new WaitForSeconds(orderConfig.OrderSpawnDelay);
                continue;
            }

            RecipeSO order = GetRandomOrder();
            activeOrders.Add(order);
            Debug.Log("order spawned here is the recipe: " + order.RecipeName);
            yield return new WaitForSeconds(orderConfig.OrderSpawnDelay);
        }
    }

    private RecipeSO GetRandomOrder()
    {
        int randomIndex = Random.Range(0, recipes.Count);
        return recipes[randomIndex];
    }

    public bool RecipeHasOrdered(string recipeID)
    {
        RecipeSO recipe = orderConfig.RecipeList.Find(x => x.RecipeID == recipeID);

        return activeOrders.Contains(recipe);
    }
}
