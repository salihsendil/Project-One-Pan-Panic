using Zenject;

public class Tutorial_SceneInstaller_Addons : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<TutorialStepManager>().FromComponentInHierarchy().AsSingle();
    }
}