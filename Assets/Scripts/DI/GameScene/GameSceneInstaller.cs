using Zenject;

public class GameSceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<GameManager>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<InputHandler>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<OrderSystem>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<RecipeMatchEvaluator>().AsSingle().NonLazy();
        Container.Bind<UniversalPoolManager>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<SceneService>().FromComponentInHierarchy().AsSingle();

        InterfaceBindings();
        SignalBindings();
    }


    private void InterfaceBindings()
    {
        Container.BindInterfacesAndSelfTo<ScoreHandler>().AsSingle();
    }

    private void SignalBindings()
    {
        Container.DeclareSignal<CountdownTickSignal>();
        Container.DeclareSignal<GameStartedSignal>();
        Container.DeclareSignal<GameFinishedSignal>();
        Container.DeclareSignal<TogglePauseRequestSignal>();
        Container.DeclareSignal<LevelTimerTickSignal>();
        Container.DeclareSignal<OrderDeliveredSignal>();
        Container.DeclareSignal<ScoreChangedSignal>();
        Container.DeclareSignal<OrderGeneratedSignal>();
        Container.DeclareSignal<OrderExpiredSignal>();
        Container.DeclareSignal<ContainerItemDespawned>();
    }

}