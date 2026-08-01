using Zenject;

public class Game_SceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        //Hierarchy Bindings
        Container.Bind<GameManager>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<InputHandler>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<OrderManager>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<UniversalPoolManager>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<LevelStatsService>().FromComponentInHierarchy().AsSingle().NonLazy();

        //Object Bindings
        Container.Bind<RecipeMatchEvaluator>().AsSingle().NonLazy();

        //Interface Bindings
        Container.BindInterfacesAndSelfTo<ScoreHandler>().AsSingle();
        Container.BindInterfacesAndSelfTo<AudioServiceInitializer>().AsSingle().NonLazy();
    }
}