using UnityEngine;

[RequireComponent(typeof(ItemInteractionModule))]
public class HoldingCounter : BaseCounter
{
    private ItemSocket itemSocket;

    private void Awake()
    {
        TryGetComponent(out itemSocket);

        TryGetComponent(out ItemInteractionModule itemInteractionModule);
        counterModules[0] = itemInteractionModule;
    }
}