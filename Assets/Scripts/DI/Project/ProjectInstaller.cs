using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField] private GameObject loadingCanvasPrefab;
    [SerializeField] private GameObject audioProfilePrefab;
    [SerializeField] private GameObject sfxServicePrefab;

    public override void InstallBindings()
    {
        Application.targetFrameRate = 60;

        //Interface Bindings
        Container.BindInterfacesAndSelfTo<SaveSystem>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<WardrobeManager>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<CurrencyManager>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<LevelDataService>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<GameSettingsService>().AsSingle().NonLazy();

        //Instantiate Object Bindings
        Container.Bind<SceneService>().FromNewComponentOnNewGameObject().AsSingle();

        //Prefab Instantiating

        Container.Bind<LoadingPanelHandler>().FromComponentInNewPrefab(loadingCanvasPrefab).AsSingle().NonLazy();
        Container.Bind<AudioService>().FromComponentInNewPrefab(audioProfilePrefab).AsSingle().NonLazy();
        Container.Bind<SFXService>().FromComponentInNewPrefab(sfxServicePrefab).AsSingle().NonLazy();

        //Signal Bindings
        SignalBusInstaller.Install(Container);
        Container.DeclareSignal<SceneFullyLoadedSignal>();
    }
}