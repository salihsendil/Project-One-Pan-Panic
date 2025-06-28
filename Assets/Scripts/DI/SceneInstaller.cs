using Zenject;

public class SceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<InputHandler>().FromComponentInHierarchy().AsSingle();
        Container.Bind<GameManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<OrderManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<GameStatsManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<KitchenItemPoolManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<ContainerDispenserSystem>().FromComponentInHierarchy().AsSingle();
        Container.Bind<KitchenItemRestorer>().AsSingle();
    }
}
