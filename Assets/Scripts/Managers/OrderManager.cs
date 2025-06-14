using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    private OrderGenerator orderGenerator;
    [SerializeField] private List<RecipeSO> currentOrderList = new List<RecipeSO>();

    public List<RecipeSO> CurrentOrderList { get => currentOrderList; }

    private void Awake()
    {
        if (!TryGetComponent<OrderGenerator>(out orderGenerator))
        {
            Debug.LogError(this.name + " cannot find any Order Generator references!");
        }
    }

    private void Start()
    {
        GenerateOrder();
    }

    private void GenerateOrder()
    {
        RecipeSO recipeSO = orderGenerator.GenerateRandomRecipe();
        currentOrderList.Add(recipeSO);
    }
}
