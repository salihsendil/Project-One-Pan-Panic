using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField] private GameObject loadingCanvas;

    public override void InstallBindings()
    {
        Application.targetFrameRate = 60;

        //Interface Bindings
        Container.BindInterfacesAndSelfTo<SaveSystem>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<LevelDataService>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<WardrobeManager>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<GameSettingsService>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<CurrencyManager>().AsSingle().NonLazy();

        //Instantiate Object Bindings
        Container.Bind<SceneService>().FromNewComponentOnNewGameObject().AsSingle();
        Container.Bind<LoadingPanelHandler>().FromComponentInNewPrefab(loadingCanvas).AsSingle().NonLazy();

        //Signal Bindings
        SignalBusInstaller.Install(Container);
        Container.DeclareSignal<SceneFullyLoadedSignal>();
    }
}