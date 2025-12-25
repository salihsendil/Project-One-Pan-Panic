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

    public override void Interact(PlayerCarryingController player)
    {
        foreach (var module in counterModules)
        {
            if (module == null) { continue; }

            if (module.TryInteract(player))
            {
                //itemSocket.SetItem(player.RemoveItem());??? - is empty just for now
                break;
            }
        }
    }
}
