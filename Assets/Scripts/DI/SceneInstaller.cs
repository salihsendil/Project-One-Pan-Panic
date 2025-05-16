using Zenject;

public class SceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<InputHandler>().FromComponentInHierarchy().AsSingle();
    }
}
