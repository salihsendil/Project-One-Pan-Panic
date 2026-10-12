using Zenject;

public class Game_Scene_SignalInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        //Order Signals
        Container.DeclareSignal<OrderGeneratedSignal>();
        Container.DeclareSignal<OrderExpiredSignal>();
        Container.DeclareSignal<OrderDeliveredSignal>();

        //Stats Signals
        Container.DeclareSignal<ScoreChangedSignal>();
    }
}