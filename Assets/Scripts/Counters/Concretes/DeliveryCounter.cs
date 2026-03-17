using UnityEngine;

[RequireComponent(typeof(DeliveryModule))]
public class DeliveryCounter : BaseCounter
{
    private void Awake()
    {
        TryGetComponent(out DeliveryModule deliveryModule);
        counterModules[0] = deliveryModule;
    }
}
