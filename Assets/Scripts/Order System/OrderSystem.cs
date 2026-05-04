using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class OrderSystem : MonoBehaviour
{
    //Zenject
    [Inject] private SignalBus signalBus;
    [Inject] private OrderConfigSO orderConfig;

    //Game State Check
    [SerializeField] private bool isGamePlaying = false;

    //Order List
    private int orderCounter = 0;
    private List<Order> activeOrders = new();
    private List<RecipeSO> recipes => orderConfig.RecipeList;

    private void OnEnable()
    {
        signalBus.Subscribe<GameStartedSignal>(StartGenerateOrder);
        signalBus.Subscribe<GameFinishedSignal>(StopGenerateOrder);
    }

    private void OnDisable()
    {
        signalBus.Unsubscribe<GameStartedSignal>(StartGenerateOrder);
        signalBus.Unsubscribe<GameFinishedSignal>(StopGenerateOrder);
    }

    void Start()
    {
        StartCoroutine(TrySpawnOrderPeriodically());
    }

    private void Update()
    {
        TickOrderTimers();
    }
    private void StartGenerateOrder()
    {
        SetOrderSpawnAvailability(true);
    }

    private void StopGenerateOrder()
    {
        SetOrderSpawnAvailability(false);
    }

    private void SetOrderSpawnAvailability(bool canSpawn)
    {
        isGamePlaying = canSpawn;
    }

    IEnumerator TrySpawnOrderPeriodically()
    {
        while (true)
        {
            if (!isGamePlaying) { yield return new WaitUntil(() => isGamePlaying); }

            if (orderConfig.MaxActiveOrderCount <= activeOrders.Count) { yield return null; }

            Order order = GetRandomOrder();
            activeOrders.Add(order);
            signalBus.Fire(new OrderGeneratedSignal(order));

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

    private void TickOrderTimers()
    {
        if (!isGamePlaying) { return; }

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