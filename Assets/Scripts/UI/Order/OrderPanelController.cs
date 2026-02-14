using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class OrderPanelController : MonoBehaviour
{
    //Zenject
    [Inject] private SignalBus signalBus;
    [Inject] private UniversalPoolManager poolManager;

    //References
    [SerializeField] private GameObject orderPanel;


    //Data
    private Dictionary<int, OrderCardView> cards = new();


    private void OnEnable()
    {
        signalBus.Subscribe<OrderGeneratedSignal>(OnNewOrderGenerated);
        signalBus.Subscribe<OrderDeliveredSignal>(OnOrderDelivered);
        signalBus.Subscribe<OrderExpiredSignal>(OnOrderExpired);
    }

    private void OnDisable()
    {
        signalBus.Unsubscribe<OrderGeneratedSignal>(OnNewOrderGenerated);
        signalBus.Unsubscribe<OrderDeliveredSignal>(OnOrderDelivered);
        signalBus.Unsubscribe<OrderExpiredSignal>(OnOrderExpired);
    }


    private void OnNewOrderGenerated(OrderGeneratedSignal signal)
    {
        OrderCardView orderCard = poolManager.SpawnWithConfig<OrderCardView, Order>
                             (UniversalPoolEntryType.OrderCardView, signal.Order);
        cards[signal.Order.ID] = orderCard;
        orderCard.gameObject.transform.SetParent(orderPanel.transform, false);
    }

    private void OnOrderDelivered(OrderDeliveredSignal signal)
    {
        if (cards.TryGetValue(signal.Order.ID, out OrderCardView cardView))
        {
            cards.Remove(signal.Order.ID);
            PoolItemCleaner.RestoreAndReturn(cardView, poolManager);
        }
    }

    private void OnOrderExpired(OrderExpiredSignal signal)
    {
        if (cards.TryGetValue(signal.Order.ID, out OrderCardView cardView))
        {
            cards.Remove(signal.Order.ID);
            PoolItemCleaner.RestoreAndReturn(cardView, poolManager);
        }
    }
}