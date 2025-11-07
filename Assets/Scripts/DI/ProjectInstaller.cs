using Zenject;

public class ProjectInstaller : MonoInstaller
{
    public GameManager GameManagerPrefab;
    public GameStatsManager GameStatsManagerPrefab;
    public GeneralUIService generalUIServicePrefab;


    public override void InstallBindings()
    {
        var gameManager = Container.InstantiatePrefabForComponent<GameManager>(GameManagerPrefab);
        Container.Bind<GameManager>().FromInstance(gameManager).AsSingle().NonLazy();

        var gameStatsManager = Container.InstantiatePrefabForComponent<GameStatsManager>(GameStatsManagerPrefab);
        Container.Bind<GameStatsManager>().FromInstance(gameStatsManager).AsSingle().NonLazy();

        var uiService = Container.InstantiatePrefabForComponent<GeneralUIService>(generalUIServicePrefab);
        Container.Bind<GeneralUIService>().FromInstance(uiService).AsSingle().NonLazy();
    }
}
