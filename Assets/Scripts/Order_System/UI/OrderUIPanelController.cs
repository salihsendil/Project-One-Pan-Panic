using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class OrderUIPanelController : MonoBehaviour
{
    [Inject] private SignalBus signalBus;

    [SerializeField] private OrderConfigSO orderConfig;
    [SerializeField] private OrderUICard cardPrefab;
    private Stack<OrderUICard> cardStack = new();
    private Dictionary<int, OrderUICard> activeCards = new();

    private void OnEnable()
    {
        signalBus.Subscribe<OrderGeneratedSignal>(OnNewOrderGenerated);
        signalBus.Subscribe<OrderDeliveredSignal>(OnOrderDelivered);
    }

    private void OnDisable()
    {

        signalBus.Unsubscribe<OrderGeneratedSignal>(OnNewOrderGenerated);
        signalBus.Unsubscribe<OrderDeliveredSignal>(OnOrderDelivered);
    }

    private void Start()
    {
        for (int i = 0; i < orderConfig.MaxOrderCount; i++)
        {
            OrderUICard card = Instantiate(cardPrefab, transform, false);
            cardStack.Push(card);
            card.gameObject.SetActive(false);
        }
    }

    private void OnNewOrderGenerated(OrderGeneratedSignal signal)
    {
        if (!cardStack.TryPop(out OrderUICard card))
        {
            card = Instantiate(cardPrefab, transform, false);
        }

        activeCards[signal.Order.ID] = card;
        card.Initialize(signal.Order);
    }

    private void OnOrderDelivered(OrderDeliveredSignal signal)
    {
        if (!activeCards.TryGetValue(signal.Order.ID, out OrderUICard card)) return;

        if (activeCards.Remove(signal.Order.ID))
        {
            card.Disable();
            cardStack.Push(card);
        }
    }
}
