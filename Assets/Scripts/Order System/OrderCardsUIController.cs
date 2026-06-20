using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class OrderCardsUIController : MonoBehaviour
{
    [Inject] private SignalBus signalBus;
    [Inject] private UniversalPoolManager poolManager;

    [SerializeField] private Dictionary<int, OrderUICard> cards = new();

    private void OnEnable()
    {
        signalBus.Subscribe<OrderGeneratedSignal>(OnOrderGenerated);
        signalBus.Subscribe<OrderDeliveredSignal>(OnOrderDelivered);
    }

    private void OnDisable()
    {
        signalBus.Unsubscribe<OrderGeneratedSignal>(OnOrderGenerated);
        signalBus.Unsubscribe<OrderDeliveredSignal>(OnOrderDelivered);
    }

    public void OnOrderGenerated(OrderGeneratedSignal signal)
    {
        OrderUICard card = poolManager.SpawnWithConfig<OrderUICard, Order>
                           (ItemType.OrderCard, signal.Order);
        card.transform.SetParent(transform, false);
        cards[signal.Order.OrderID] = card;
    }

    public async void OnOrderDelivered(OrderDeliveredSignal signal)
    {
        if (!cards.TryGetValue(signal.Order.OrderID, out OrderUICard card)) return;

        await card.OrderDeliveredAsync();

        cards.Remove(signal.Order.OrderID);
        poolManager.Despawn(card);
    }
}
