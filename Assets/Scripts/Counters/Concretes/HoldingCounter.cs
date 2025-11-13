
public class HoldingCounter : BaseCounter
{    
    public override void Interact(PlayerCarryingController player)
    {
        foreach (var module in counterModules)
        {
            if (module.TryInteract(player))
            {
                break;
            }
        }
    }
}
