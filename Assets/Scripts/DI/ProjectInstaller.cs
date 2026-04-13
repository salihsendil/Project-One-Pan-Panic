using Zenject;

public class ProjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<SaveSystem>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<WardrobeManager>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<GameSettingsService>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<CurrencyManager>().AsSingle().NonLazy();
        Container.Bind<SceneService>().FromNewComponentOnNewGameObject().AsSingle();

        SignalInstallers();
    }

    private void SignalInstallers()
    {
        SignalBusInstaller.Install(Container);        
    }
}