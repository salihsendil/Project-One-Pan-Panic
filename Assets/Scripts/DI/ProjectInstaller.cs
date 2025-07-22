using Zenject;

public class ProjectInstaller : MonoInstaller
{
    public GameManager GameManagerPrefab;
    public GameStatsManager GameStatsManagerPrefab;
    public UICommandController UICommandControllerPrefab;
    public SceneService SceneServicePrefab;


    public override void InstallBindings()
    {
        var sceneService = Container.InstantiatePrefabForComponent<ISceneService>(SceneServicePrefab);
        Container.Bind<ISceneService>().FromInstance(sceneService).AsSingle().NonLazy();

        Container.Bind<LoadSceneCommand>().AsSingle().NonLazy();
        Container.Bind<PopupCommand>().AsSingle().NonLazy();
        Container.Bind<UICommandFactory>().AsSingle().NonLazy();

        var gameManager = Container.InstantiatePrefabForComponent<GameManager>(GameManagerPrefab);
        Container.Bind<GameManager>().FromInstance(gameManager).AsSingle().NonLazy();

        var gameStatsManager = Container.InstantiatePrefabForComponent<GameStatsManager>(GameStatsManagerPrefab);
        Container.Bind<GameStatsManager>().FromInstance(gameStatsManager).AsSingle().NonLazy();

        var uiCommandController = Container.InstantiatePrefabForComponent<UICommandController>(UICommandControllerPrefab);
        Container.Bind<UICommandController>().FromInstance(uiCommandController).AsSingle().NonLazy();
    }
}
