using UnityEngine;

[RequireComponent(typeof(ItemInteractionModule))]
public class HoldingCounter : BaseCounter
{
    private void Awake()
    {
        TryGetComponent(out ItemInteractionModule itemInteractionModule);
        counterModules[0] = itemInteractionModule;
    }
}
