using UnityEngine;
using Zenject;

public class KitchenItemRefactorInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<InputHandler>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<UniversalPoolManager>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<RecipeMatchEvaluator>().AsSingle().NonLazy(); ;
    }
}