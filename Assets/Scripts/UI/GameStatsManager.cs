using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameStatsManager : MonoBehaviour
{
    [Inject] private GameManager gameManager;
 
    [Header("Timer")]
    [SerializeField] private float time;
    public event Action<float> OnTimerValueChanged;

    [Header("Order")]
    [SerializeField] private List<RecipeSO> currentOrderRecipe = new List<RecipeSO>();//just for debugging, DELETE!!
    [SerializeField] private List<OrderInstance> currentOrderInstances = new List<OrderInstance>(5);
    public event Action<OrderInstance> OnOrderListValuesChanged;
    public List<OrderInstance> CurrentOrderInstances { get => currentOrderInstances; }

    private void Awake()
    {
        time = gameManager.Settings.GameTime;
    }

    private void Update()
    {
        time -= Time.deltaTime;
        OnTimerValueChanged?.Invoke(time);
    }

    public void AddOrder(OrderInstance newOrder)
    {
        currentOrderRecipe.Add(newOrder.RecipeSO); //just for debugging, DELETE!!
        currentOrderInstances.Add(newOrder);
        OnOrderListValuesChanged?.Invoke(newOrder);
    }

    public void DeleteOrder(OrderInstance order)
    {
        currentOrderInstances.Remove(order);
        currentOrderRecipe.Remove(order.RecipeSO); //just for debugging, DELETE!!
    }
}
