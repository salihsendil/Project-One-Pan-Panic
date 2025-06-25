using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class OrderManager : MonoBehaviour
{
    //REFERENCES
    [Inject] private GameManager gameManager;
    [Inject] private GameStatsManager gameStatsManager;
    private OrderGenerator orderGenerator;
    private Coroutine orderCoroutine;
    public event Action<OrderInstance> OnOrderListValueAdded;
    public event Action<OrderInstance> OnOrderListValueDeleted;

    [Header("Orders")]
    [SerializeField] private List<OrderInstance> orderInstances = new List<OrderInstance>();
    [SerializeField] private List<RecipeSO> currentOrderRecipes = new List<RecipeSO>(); //just for debugging, DELETE
    [SerializeField] private int currentOrderCount = 0;

    public List<OrderInstance> OrderInstances => orderInstances;

    public int CurrentOrderCount => currentOrderCount;

    private void Awake()
    {
        FillTheOrderInstanceList();

        if (!TryGetComponent(out orderGenerator))
        {
            Debug.LogError(this.name + " cannot find any Order Generator references!");
        }
    }

    private void Start()
    {
        orderCoroutine = StartCoroutine(GenerateOrder());
    }

    private IEnumerator GenerateOrder()
    {
        yield return new WaitForSeconds(gameManager.GameConfig.FirstOrderDelay);

        while (true)
        {
            if (currentOrderCount < gameManager.GameConfig.MAX_ORDER_COUNT)
            {
                for (int i = 0; i < orderInstances.Count; i++)
                {
                    if (orderInstances[i].HasOrderExpired)
                    {
                        orderInstances[i].SetOrderInstance(orderGenerator.GenerateRandomRecipe());
                        currentOrderRecipes.Add(orderInstances[i].RecipeSO); //just for debugging, DELETE;
                        orderInstances[i].OnOrderExpired -= OrderExpired;
                        orderInstances[i].OnOrderExpired += OrderExpired;
                        OnOrderListValueAdded?.Invoke(orderInstances[i]);
                        currentOrderCount++;
                        break;
                    }
                }
            }
            yield return new WaitForSeconds(UnityEngine.Random.Range(gameManager.GameConfig.MinOrderDelay, gameManager.GameConfig.maxOrderDelay));
        }
    }

    public void OrderDelivered(OrderInstance order)
    {
        gameStatsManager.UpdateScore(order.RecipeSO.successfulOrderPoint);
        DeActivateOrder(order);
    }

    public void OrderExpired(OrderInstance order)
    {
        gameStatsManager.UpdateScore(order.RecipeSO.failedOrderPenaltyPoint);
        DeActivateOrder(order);
    }

    private void DeActivateOrder(OrderInstance order)
    {
        var target = orderInstances.Find(x => x == order);
        if (target != null)
        {
            currentOrderRecipes.Remove(target.RecipeSO);
            OnOrderListValueDeleted?.Invoke(order);
            order.ClearOrderInstance();
            currentOrderCount--;
            order.OnOrderExpired -= OrderExpired;
            return;
        }
    }

    private void FillTheOrderInstanceList()
    {
        for (int i = 0; i < gameManager.GameConfig.MAX_ORDER_COUNT; i++)
        {
            OrderInstance orderInstance = new OrderInstance();
            orderInstances.Add(orderInstance);
        }
    }
}
