public struct OrderExpiredSignal
{
    public Order Order;

    public OrderExpiredSignal(Order order)
    {
        Order = order;
    }
}