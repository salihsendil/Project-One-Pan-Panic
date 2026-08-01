using UnityEngine;
using Zenject;
using System.Collections.Generic;
using System.Collections;
public class OrderManager : MonoBehaviour
{
    [Inject] private OrderConfigSO orderConfig;
    [Inject] private SFXService sfxService;
    [Inject] private SignalBus signalBus;

    [SerializeField] private int orderCounter;
    [SerializeField] private List<Order> activeOrders = new();
    [SerializeField] private bool isPlaying;
    [SerializeField] private Coroutine orderCoroutine;

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

    private void Update()
    {
        TickOrdersTimes();
    }

    private void StartGame()
    {
        SetGameState(true);
        orderCoroutine = StartCoroutine(TrySpawnOrderPeriodically());
    }

    private void StopGame()
    {
        SetGameState(false);
        StopCoroutine(orderCoroutine);
    }

    private IEnumerator TrySpawnOrderPeriodically()
    {
        while (isPlaying)
        {
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
        //if (activeOrders.Count <= 0) return;

        for (int i = activeOrders.Count - 1; i >= 0; i--)
        {
            Order order = activeOrders[i];

            order.Tick(Time.deltaTime);

            if (order.IsExpired)
            {
                sfxService.PlaySFXOneShot(SFXType.OrderFail);
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
        sfxService.PlaySFXOneShot(SFXType.NewOrder);
        signalBus.Fire(new OrderGeneratedSignal(order));
        Debug.Log(order.Recipe.RecipeID);
    }

    private RecipeSO GetRandomRecipe()
    {
        int random = Random.Range(0, orderConfig.RecipeEntries.Count);
        orderCounter++;
        return orderConfig.RecipeEntries[random].Recipe;
    }

    public void GenerateOrder(RecipeSO recipeSO)
    {
        Order order = new(orderCounter, recipeSO);
        activeOrders.Add(order);
        signalBus.Fire(new OrderGeneratedSignal(order));
    }

    public bool TryCompleteOrder(RecipeSO recipe)
    {
        foreach (var order in activeOrders)
        {
            if (order.Recipe == recipe)
            {
                activeOrders.Remove(order);
                sfxService.PlaySFXOneShot(SFXType.OrderDelivered);
                signalBus.Fire(new OrderDeliveredSignal(GameplayEvent.OrderDelivered, order));
                return true;
            }
        }

        return false;
    }
}
