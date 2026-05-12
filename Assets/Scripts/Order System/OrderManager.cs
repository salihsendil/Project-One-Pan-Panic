using UnityEngine;
using Zenject;
using System.Collections.Generic;
using System.Collections;
public class OrderManager : MonoBehaviour
{
    [Inject] private OrderConfigSO orderConfig;
    [Inject] private SignalBus signalBus;

    [SerializeField] private int orderCounter;
    [SerializeField] private List<Order> activeOrders = new();
    [SerializeField] public bool isPlaying;

    private void OnEnable()
    {
        signalBus.Subscribe<GameStartedSignal>(StartGame);
        signalBus.Subscribe<GameFinishedSignal>(StopGame);
    }

    private void OnDisable()
    {
        signalBus.Unsubscribe<GameStartedSignal>(StartGame);
        signalBus.Unsubscribe<GameFinishedSignal>(StopGame);
    }

    private void SetGameState(bool value) => isPlaying = value;
    private void StartGame() => SetGameState(true);
    private void StopGame() => SetGameState(false);

    private void Start()
    {
        StartCoroutine(TrySpawnOrderPeriodically());
    }

    private void Update()
    {
        TickOrdersTimes();
    }

    private IEnumerator TrySpawnOrderPeriodically()
    {
        while (true)
        {
            if (!isPlaying) { yield return new WaitUntil(() => isPlaying); }

            yield return new WaitForSeconds(orderConfig.StartOrderSpawnDelay);

            if (activeOrders.Count >= orderConfig.MaxActiveOrderCount)
            {
                Debug.Log("max sayýda sipariþ var.");
                yield return new WaitForSeconds(orderConfig.OrderSpawnDelay);
                continue;
            }

            GenerateOrder();
            yield return new WaitForSeconds(orderConfig.OrderSpawnDelay);
        }
    }

    private void TickOrdersTimes()
    {
        if (!isPlaying) return;

        for (int i = activeOrders.Count - 1; i >= 0; i--)
        {
            Order order = activeOrders[i];

            order.Tick(Time.deltaTime);

            if (order.IsExpired)
            {
                signalBus.Fire(new OrderExpiredSignal(order.Recipe.PenaltyScore));

                activeOrders.RemoveAt(i);

                order.SetOrderAgain();

                activeOrders.Add(order);
                Debug.Log(order.OrderID + " " + order.Recipe.RecipeID + " bunun süresi bitti");
            }
        }
    }

    private void GenerateOrder()
    {
        RecipeSO recipe = GetRandomRecipe();
        Order order = new(orderCounter, recipe);
        activeOrders.Add(order);
        signalBus.Fire(new OrderGeneratedSignal(order));
        Debug.Log(order.Recipe.RecipeID);
    }

    private RecipeSO GetRandomRecipe()
    {
        int random = Random.Range(0, orderConfig.RecipeEntries.Count);
        orderCounter++;
        return orderConfig.RecipeEntries[random].Recipe;
    }

    public bool TryCompleteOrder(RecipeSO recipe)
    {
        foreach (var order in activeOrders)
        {
            if (order.Recipe == recipe)
            {
                activeOrders.Remove(order);
                signalBus.Fire(new OrderDeliveredSignal(order));
                return true;
            }
        }

        return false;
    }
}
