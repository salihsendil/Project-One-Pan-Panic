using System;
using UnityEngine;
using Zenject;

public class Tutorial_SceneInstaller_Addons : MonoInstaller
{
    [SerializeField] private GameObject objectivePointerPrefab;

    public override void InstallBindings()
    {
        PrefabBindings();
        ContainerBindings();
        SignalBindings();
    }

    private void PrefabBindings()
    {
        Container.Bind<ObjectivePointer>().FromComponentInNewPrefab(objectivePointerPrefab).AsSingle().NonLazy();
    }

    private void ContainerBindings()
    {
        Container.Bind<TutorialStepManager>().FromComponentInHierarchy().AsSingle();
    }

    private void SignalBindings()
    {
        Container.DeclareSignal<NewTutorialStepSignal>();
    }
}