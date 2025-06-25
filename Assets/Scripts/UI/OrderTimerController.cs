using UnityEngine;
using Zenject;

public class OrderTimerController : MonoBehaviour
{
    [Inject] private OrderManager orderManager;

    void Update()
    {
        for (int i = 0; i < orderManager.OrderInstances.Count; i++)
        {
            if (!orderManager.OrderInstances[i].HasOrderExpired)
            {
                orderManager.OrderInstances[i].Tick(Time.deltaTime);
            }
        }
    }
}
