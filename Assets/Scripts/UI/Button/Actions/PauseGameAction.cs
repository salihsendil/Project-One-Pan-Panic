using UnityEngine;
using Zenject;

[RequireComponent(typeof(UIButtonHandler))]
public class PauseGameAction : BaseUIAction
{
    //Zenject
    [Inject] SignalBus signalBus;

    public override void Execute()
    {
        signalBus.Fire<TogglePauseRequestSignal>();
    }
}
