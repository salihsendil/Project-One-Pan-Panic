using Zenject;

public class Level_Signal_Installer : MonoInstaller
{
    public override void InstallBindings()
    {
        //Game Phase Signal
        Container.DeclareSignal<GamePhaseChangedSignal>();

        //Order Signals
        Container.DeclareSignal<OrderGeneratedSignal>();
        Container.DeclareSignal<OrderExpiredSignal>();
        Container.DeclareSignal<OrderDeliveredSignal>();

        //Stats Signals
        Container.DeclareSignal<ScoreChangedSignal>();
    }
}