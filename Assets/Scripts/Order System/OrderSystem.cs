using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class OrderSystem : MonoBehaviour
{
    //Zenject
    [Inject] private SignalBus signalBus;

    //Debug
    [SerializeField] private bool CanSpawnOrder = true;

    //Config
    [SerializeField] private OrderConfigSO orderConfig;
    public OrderConfigSO OrderConfig { get => orderConfig; }

    //Order List
    private int orderCounter = 0;
    private List<Order> activeOrders = new();
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

    private void Update()
    {
        for (int i = activeOrders.Count - 1; i >= 0; i--)
        {
            activeOrders[i].TickTime(Time.deltaTime);

            if (activeOrders[i].IsExpired())
            {
                signalBus.Fire(new OrderExpiredSignal(activeOrders[i]));
                activeOrders.RemoveAt(i);
            }
        }
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

            Order order = GetRandomOrder();
            activeOrders.Add(order);
            signalBus.Fire(new OrderGeneratedSignal(order));

            Debug.Log("order spawned here is the recipe: " + order.Recipe.RecipeName);

            yield return new WaitForSeconds(orderConfig.OrderSpawnDelay);
        }
    }

    private Order GetRandomOrder()
    {
        int randomIndex = Random.Range(0, recipes.Count);
        Order order = new Order(orderCounter, recipes[randomIndex]);
        orderCounter++;
        return order;
    }

    public bool TryCompleteOrder(RecipeSO recipe, out Order order)
    {
        order = default;
        foreach (var item in activeOrders)
        {
            if (recipe == item.Recipe)
            {
                order = item;
                activeOrders.Remove(order);
                return true;
            }
        }
        return false;
    }
}