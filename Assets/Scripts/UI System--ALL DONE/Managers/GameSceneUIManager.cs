using System.Collections.Generic;
using Zenject;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneUIManager : SceneManagerBase
{
    //REFERENCES
    [Inject] private GameManager gameManager;
    //[Inject] private OrderManager orderManager;

    [Header("Order Display")]
    [SerializeField] private Vector2 orderDisplayStartPosition = new Vector2(50, -50);
    [SerializeField] private Vector2 orderDisplayOffset = new Vector2(0, -120);
    [SerializeField] private List<OrderDisplay> orderDisplays = new List<OrderDisplay>();

    [Header("Pause Menu")]
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Image musicButton;
    [SerializeField] private Image soundButton;

    private void Start()
    {
        pausePanel.SetActive(false);

        FillOrderDisplayList();

        //orderManager.OnOrderListValueAdded += ActiveOrderPanel;

        //orderManager.OnOrderListValueDeleted += DisableOrderPanel;
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
            orderDisplays[i].Image.color = Color.clear;
        }
    }

    public override void TogglePausePanel()
    {
        Time.timeScale = pausePanel.activeInHierarchy ? 1 : 0;
        pausePanel.SetActive(!pausePanel.activeInHierarchy);
    }

    public override void ToggleMusic(UIActionButtonSO buttonData)
    {
        musicButton.sprite = (musicButton.sprite == buttonData.toggleOnSprite) ? buttonData.toggleOffSprite : buttonData.toggleOnSprite;
    }

    public override void ToggleSound(UIActionButtonSO buttonData)
    {
        soundButton.sprite = (soundButton.sprite == buttonData.toggleOnSprite) ? buttonData.toggleOffSprite : buttonData.toggleOnSprite;
    }
}

