using Zenject;

public class ProjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<SaveSystem>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<WardrobeManager>().AsSingle().NonLazy();
        Container.Bind<CurrencyManager>().AsSingle();

        SignalInstallers();
    }

    private void SignalInstallers()
    {
        SignalBusInstaller.Install(Container);        
    }
}