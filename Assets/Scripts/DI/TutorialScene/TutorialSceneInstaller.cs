using Zenject;

public class TutorialSceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<InputHandler>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<UniversalPoolManager>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<OrderManager>().FromComponentInHierarchy().AsSingle().NonLazy();

        Container.Bind<RecipeMatchEvaluator>().AsSingle().NonLazy();
    }
}