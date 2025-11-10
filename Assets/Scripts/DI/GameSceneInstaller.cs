using UnityEngine;
using Zenject;

public class GameSceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<InputHandler>().FromComponentInHierarchy().AsSingle();
    }
}