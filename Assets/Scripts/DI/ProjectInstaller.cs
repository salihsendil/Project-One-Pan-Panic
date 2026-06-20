using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Application.targetFrameRate = 60;

        //Interface Bindings
        Container.BindInterfacesAndSelfTo<SaveSystem>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<WardrobeManager>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<GameSettingsService>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<CurrencyManager>().AsSingle().NonLazy();

        //Instantiate Object Bindings
        Container.Bind<SceneService>().FromNewComponentOnNewGameObject().AsSingle();

        //Signal Bindings
        SignalBusInstaller.Install(Container);
    }
}