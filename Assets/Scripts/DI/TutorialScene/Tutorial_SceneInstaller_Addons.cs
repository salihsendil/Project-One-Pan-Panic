using UnityEngine;
using Zenject;

public class Tutorial_SceneInstaller_Addons : MonoInstaller
{
    [SerializeField] private GameObject objectivePointerPrefab;

    public override void InstallBindings()
    {
        PrefabBindings();
        HierarchyBindings();
        SignalBindings();
    }

    private void PrefabBindings()
    {
        Container.Bind<ObjectivePointer>().FromComponentInNewPrefab(objectivePointerPrefab).AsSingle().NonLazy();
    }

    private void HierarchyBindings()
    {
        Container.Bind<TutorialStepManager>().FromComponentInHierarchy().AsSingle();
    }

    private void SignalBindings()
    {
        Container.DeclareSignal<NewTutorialStepSignal>();
    }
}