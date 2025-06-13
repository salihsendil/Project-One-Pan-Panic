using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    private OrderGenerator orderGenerator;
    [SerializeField] private List<RecipeSO> currentOrderList = new List<RecipeSO>();

    public List<RecipeSO> CurrentOrderList { get => currentOrderList; }

    private void Awake()
    {
        TryGetComponent<OrderGenerator>(out orderGenerator);
    }

    private void Start()
    {
        currentOrderList.Add(orderGenerator.GenerateRandomRecipe());
        currentOrderList.Add(orderGenerator.GenerateRandomRecipe());
    }

}
