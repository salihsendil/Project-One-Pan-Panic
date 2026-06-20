using UnityEngine;
using Zenject;

public class Tutorial_SceneInstaller_Addons : MonoInstaller
{
    public override void InstallBindings()
    {
        SignalBindings();
    }

    private void SignalBindings()
    {
        Container.DeclareSignal<NewTutorialStepSignal>();
    }
}