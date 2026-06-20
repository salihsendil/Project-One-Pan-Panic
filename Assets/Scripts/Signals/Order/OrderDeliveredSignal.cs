public struct OrderDeliveredSignal
{
    public GameplayEvent GameplayEvent;
    public Order Order;

    public OrderDeliveredSignal(GameplayEvent gameplayEvent, Order order)
    {
        GameplayEvent = gameplayEvent;
        Order = order;
    }
}


