using Zenject;

public class ProjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<SaveSystem>().AsSingle().NonLazy();
        Container.Bind<CurrencyManager>().AsSingle().NonLazy();
        Container.Bind<SceneService>().FromNewComponentOnNewGameObject().AsSingle();
        Container.BindInterfacesAndSelfTo<WardrobeManager>().AsSingle().NonLazy();


        SignalInstallers();
    }

    private void SignalInstallers()
    {
        SignalBusInstaller.Install(Container);        
    }
}