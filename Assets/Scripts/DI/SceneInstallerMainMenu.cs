using UnityEngine;
using Zenject;

public class SceneInstallerMainMenu : MonoInstaller
{

    public override void InstallBindings()
    {
        foreach (var buttonAction in FindObjectsByType<UIButtonAction>(FindObjectsSortMode.None))
        {
            Container.Inject(buttonAction);
        }
    }
}
