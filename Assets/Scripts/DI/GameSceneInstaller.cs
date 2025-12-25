using Zenject;

public class GameSceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<InputHandler>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<OrderSystem>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<RecipeMatchEvaluator>().AsSingle().NonLazy();
        Container.Bind<KitchenItemPoolManager>().FromComponentInHierarchy().AsSingle().NonLazy();
    }
}