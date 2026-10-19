using Zenject;

public class Tutorial_Level_Installer_Addons : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<TutorialStepManager>().FromComponentInHierarchy().AsSingle();
    }
}