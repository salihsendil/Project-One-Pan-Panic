using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField] private GameObject loadingCanvas;
    [SerializeField] private GameObject audioService;

    public override void InstallBindings()
    {
        Application.targetFrameRate = 60;

        //Interface Bindings
        Container.BindInterfacesAndSelfTo<SaveSystem>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<WardrobeManager>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<CurrencyManager>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<LevelDataService>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<AudioService>().FromComponentInNewPrefab(audioService).AsSingle().NonLazy();

        //Instantiate Object Bindings
        Container.Bind<SceneService>().FromNewComponentOnNewGameObject().AsSingle();
        Container.Bind<LoadingPanelHandler>().FromComponentInNewPrefab(loadingCanvas).AsSingle().NonLazy();

        //Signal Bindings
        SignalBusInstaller.Install(Container);
        Container.DeclareSignal<SceneFullyLoadedSignal>();


        //In-Game Signal (audio service subscribe to these, so they are declared here)
        Container.DeclareSignal<ItemTransferredSignal>();
        Container.DeclareSignal<IngredientAddedToContainerSignal>();
        Container.DeclareSignal<CountdownStartedSignal>();
        Container.DeclareSignal<LevelTimerTickSignal>();
    }
}