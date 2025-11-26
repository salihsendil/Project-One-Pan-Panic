using UnityEngine;

[RequireComponent(typeof(ItemInteractionModule))]
public class HoldingCounter : BaseCounter
{
    protected override void Awake()
    {
        base.Awake();
        TryGetComponent(out ItemInteractionModule itemInteractionModule);
        counterModules[0] = itemInteractionModule;
    }
}
