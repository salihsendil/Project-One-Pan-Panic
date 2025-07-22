using Zenject;

public class SceneInstallerGame : MonoInstaller
{

    public override void InstallBindings()
    {
        Container.Bind<InputHandler>().FromComponentInHierarchy().AsSingle();
        Container.Bind<OrderManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<KitchenItemPoolManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<ContainerDispenserSystem>().FromComponentInHierarchy().AsSingle();
        Container.Bind<BillboardManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<KitchenItemRestorer>().AsSingle();
    }
}
