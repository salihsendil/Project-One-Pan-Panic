using System.Collections;
using UnityEngine;

[RequireComponent(typeof(OrderViewerModule))]
public class OrderCounter : BaseCounter
{
    private OrderViewerModule orderViewerModule;
    private void Awake()
    {
        TryGetComponent(out orderViewerModule);
        counterModules[0] = orderViewerModule;
    }
}
