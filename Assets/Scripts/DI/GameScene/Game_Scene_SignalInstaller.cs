using Zenject;

public class Game_Scene_SignalInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        //Gameplay Phase Signals
        Container.DeclareSignal<CountdownStartedSignal>();
        Container.DeclareSignal<GameStartedSignal>();
        Container.DeclareSignal<TogglePauseRequestSignal>();
        Container.DeclareSignal<GameFinishedSignal>();

        //Timer Signals
        Container.DeclareSignal<CountdownTickSignal>();
        Container.DeclareSignal<LevelTimerTickSignal>();

        //Order Signals
        Container.DeclareSignal<OrderGeneratedSignal>();
        Container.DeclareSignal<OrderExpiredSignal>();
        Container.DeclareSignal<OrderDeliveredSignal>();

        //Stats Signals
        Container.DeclareSignal<ScoreChangedSignal>();


        //Kitchen Item Signals
        Container.DeclareSignal<ItemTransferredSignal>();
        Container.DeclareSignal<ItemProcessedSignal>();
        Container.DeclareSignal<IngredientAddedToContainerSignal>();
        Container.DeclareSignal<ContainerItemDespawnSignal>();
    }
}