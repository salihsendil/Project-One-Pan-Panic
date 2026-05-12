using Zenject;

public class GameSceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<GameManager>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<InputHandler>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<OrderManager>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<RecipeMatchEvaluator>().AsSingle().NonLazy();
        Container.Bind<UniversalPoolManager>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<LevelStatsService>().FromComponentInHierarchy().AsSingle().NonLazy();

        InterfaceBindings();
        SignalBindings();
    }

    private void InterfaceBindings()
    {
        Container.BindInterfacesAndSelfTo<ScoreHandler>().AsSingle();
    }

    private void SignalBindings()
    {
        Container.DeclareSignal<LevelTimerTickSignal>();
        Container.DeclareSignal<CountdownTickSignal>();
        
        Container.DeclareSignal<ScoreChangedSignal>();
        
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