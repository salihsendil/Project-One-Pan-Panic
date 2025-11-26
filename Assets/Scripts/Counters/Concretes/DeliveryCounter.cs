using UnityEngine;

[RequireComponent(typeof(DeliveryModule))]
public class DeliveryCounter : BaseCounter
{
    protected override void Awake()
    {
        base.Awake();
        TryGetComponent(out DeliveryModule deliveryModule);
        counterModules[0] = deliveryModule;
    }
}
