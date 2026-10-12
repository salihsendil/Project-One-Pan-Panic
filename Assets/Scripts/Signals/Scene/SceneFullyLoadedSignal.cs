public struct SceneFullyLoadedSignal
{
    public ScenesEnum LoadedScene;

    public SceneFullyLoadedSignal(ScenesEnum loadedScene)
    {
        LoadedScene = loadedScene;
    }
}