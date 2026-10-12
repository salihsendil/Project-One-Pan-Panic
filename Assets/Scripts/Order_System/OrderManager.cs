using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class OrderManager : MonoBehaviour
{
    [Inject] private SignalBus signalBus;

    [Header("Config")]
    [SerializeField] private OrderConfigSO orderConfig;
    private WaitForSeconds orderDelay;

    [Header("Orders")]
    [SerializeField] private Coroutine orderCoroutine;
    [SerializeField] private int orderCounter;
    [SerializeField] private List<Order> activeOrders = new();

    private void OnEnable()
    {
        signalBus.Subscribe<GamePhaseChangedSignal>(HandleGamePhaseChange);
    }

    private void OnDisable()
    {
        signalBus.Unsubscribe<GamePhaseChangedSignal>(HandleGamePhaseChange);
    }

    private void Start()
    {
        orderDelay = new WaitForSeconds(orderConfig.OrderSpawnInterval);
    }

    private void Update()
    {
        TickOrders();
    }

    //call with signal bus.
    private void HandleGamePhaseChange(GamePhaseChangedSignal signal)
    {
        switch (signal.Phase)
        {
            case GamePhase.Play:
                if (orderCoroutine == null)
                    orderCoroutine = StartCoroutine(SpawnOrderRegularIntervals());
                break;

            default:
                if (orderCoroutine != null)
                {
                    StopCoroutine(orderCoroutine);
                    orderCoroutine = null;
                }
                break;
        }
    }

    private void TickOrders()
    {
        for (int i = activeOrders.Count - 1; i >= 0; i--)
        {
            Order order = activeOrders[i];

            order.Tick(Time.deltaTime);

            if (order.IsExpired)
            {
                activeOrders.RemoveAt(i);
                order.SetOrderAgain();
                signalBus.Fire(new OrderExpiredSignal(order.Recipe.PenaltyScore));
                activeOrders.Add(order);
            }
        }
    }

    private IEnumerator SpawnOrderRegularIntervals()
    {
        while (true)
        {
            if (activeOrders.Count < orderConfig.MaxOrderCount)
            {
                SpawnOrder();
            }

            else
            {
                Debug.Log("Max sayýda sipariþ var.");
            }

            yield return orderDelay;
        }
    }

    private void SpawnOrder()
    {
        int index = Random.Range(0, orderConfig.Recipes.Count);
        RecipeSO recipe = orderConfig.Recipes[index];
        SpawnOrder(recipe);
    }

    //Its generated for tutorial section cuz it requires specific order.
    public void SpawnOrder(RecipeSO recipe)
    {
        Order order = new(orderCounter, recipe);
        activeOrders.Add(order);
        orderCounter++;
        signalBus.Fire(new OrderGeneratedSignal(order));
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