using System.Collections.Generic;
using Zenject;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    //REFERENCES
    [Inject] private GameManager gameManager;
    [Inject] private OrderManager orderManager;

    [Header("Order Display")]
    [SerializeField] private GameObject orderDisplayPanelPrefab;
    [SerializeField] private Vector2 orderDisplayStartPosition = new Vector2(50, -50);
    [SerializeField] private Vector2 orderDisplayOffset = new Vector2(300, 0);
    [SerializeField] private List<OrderDisplay> orderDisplays = new List<OrderDisplay>();

    private void Start()
    {
        FillOrderDisplayList();

        orderManager.OnOrderListValueAdded += ActiveOrderPanel;

        orderManager.OnOrderListValueDeleted += DisableOrderPanel;
    }

    private void ActiveOrderPanel(OrderInstance order)
    {
        for (int i = 0; i < orderDisplays.Count; i++)
        {
            if (orderDisplays[i].Image.color == Color.clear)
            {
                orderDisplays[i].Image.color = Color.white;

                orderDisplays[i].RectTransform.anchoredPosition = orderDisplayStartPosition + orderDisplayOffset * i;

                orderDisplays[i].ShowOrderPanel(order);

                break;
            }
        }
    }

    private void SetOrderPanelPosition()
    {
        for (int i = 0; i < orderDisplays.Count; i++)
        {
            if (orderDisplays[i].Image.color == Color.white)
            {
                orderDisplays[i].RectTransform.anchoredPosition = orderDisplayStartPosition + orderDisplayOffset * i;
            }
        }
    }

    private void DisableOrderPanel(OrderInstance order)
    {
        for (int i = 0; i < orderDisplays.Count; i++)
        {
            if (orderDisplays[i].Order.RecipeSO == order.RecipeSO)
            {
                orderDisplays[i].HideOrderPanel();

                orderDisplays[i].Image.color = Color.clear;

                ReOrderTheOrderList(i);

                break;
            }
        }

        SetOrderPanelPosition();
    }


    private void ReOrderTheOrderList(int indexToMove)
    {
        OrderDisplay temp = orderDisplays[indexToMove];

        for (int i = indexToMove; i < orderDisplays.Count - 1; i++)
        {
            orderDisplays[i] = orderDisplays[i + 1];
        }

        orderDisplays[orderDisplays.Count - 1] = temp;
    }


    private void FillOrderDisplayList()
    {
        for (int i = 0; i < gameManager.GameConfig.MAX_ORDER_COUNT; i++)
        {
            GameObject panel = Instantiate(orderDisplayPanelPrefab, transform.position, Quaternion.identity);

            panel.transform.SetParent(transform);

            panel.TryGetComponent(out OrderDisplay orderDisplay);

            orderDisplays.Add(orderDisplay);
            
            orderDisplays[i].Image.color = Color.clear;
        }
    }

}
