using UnityEngine;
using Zenject;

public class TutorialSignalInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        //Container.DeclareSignal<LevelTimerTickSignal>();
        //Container.DeclareSignal<CountdownTickSignal>();

        //Container.DeclareSignal<ScoreChangedSignal>();

        Container.DeclareSignal<ContainerItemDespawnSignal>();

        //Game State Signals
        Container.DeclareSignal<GameStartedSignal>();
        Container.DeclareSignal<TogglePauseRequestSignal>();
        Container.DeclareSignal<GameFinishedSignal>();

        //Order Signals
        Container.DeclareSignal<OrderGeneratedSignal>();
        Container.DeclareSignal<OrderExpiredSignal>();
        Container.DeclareSignal<OrderDeliveredSignal>();
    }
}