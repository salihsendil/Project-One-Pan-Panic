using System.Collections.Generic;
using Zenject;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    [Inject] private GameManager gameManager;
    [Inject] private GameStatsManager gameStatsManager;

    [Header("Order Display")]
    [SerializeField] private GameObject orderDisplayPanelPrefab;
    [SerializeField] private Vector2 orderDisplayStartPosition = new Vector2(50, -50);
    [SerializeField] private Vector2 orderDisplayOffset = new Vector2(0, 130);
    [SerializeField] private List<OrderDisplay> orderDisplays = new List<OrderDisplay>();

    private void Start()
    {
        for (int i = 0; i < gameManager.Settings.MAX_ORDER_COUNT; i++)
        {
            GameObject panel = Instantiate(orderDisplayPanelPrefab, transform.position, Quaternion.identity);
            panel.transform.SetParent(transform);
            panel.TryGetComponent(out OrderDisplay orderDisplay);
            orderDisplays.Add(orderDisplay);
            //panel.SetActive(false);
            if (panel.TryGetComponent<Image>(out var image))
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);

            }
        }

        gameStatsManager.OnOrderListValuesChanged += SetOrderPanel;
    }

    private void SetOrderPanel(OrderInstance order)
    {

        for (int i = 0; i < orderDisplays.Count; i++)
        {
            //if (orderDisplays[i].gameObject.activeInHierarchy == false)
            //{
            //    orderDisplays[i].gameObject.SetActive(true);

            //    orderDisplays[i].TryGetComponent(out RectTransform rectTransform);
            //    rectTransform.anchoredPosition = orderDisplayStartPosition - orderDisplayOffset * i;

            //    orderDisplays[i].ShowOrderPanel(order);
            //    break;
            //}

            if (orderDisplays[i].gameObject.TryGetComponent(out Image image))
            {
                if (image.color.a == 0f)
                {
                    image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);

                    orderDisplays[i].TryGetComponent(out RectTransform rectTransform);
                    rectTransform.anchoredPosition = orderDisplayStartPosition - orderDisplayOffset * i;
                    orderDisplays[i].ShowOrderPanel(order);
                    break;
                }
            }
        }
    }

}
