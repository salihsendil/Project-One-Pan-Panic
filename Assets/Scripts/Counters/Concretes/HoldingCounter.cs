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

    public override bool TryGetItemIcon()
    {
        if (!itemSocket.HasItem()) { return false; }

        if (!itemSocket.GetItem().TryGetComponent(out IInfoProvider provider))
        {
            return false;
        }
        provider.GetDataInfo();

        return true;
    }
}