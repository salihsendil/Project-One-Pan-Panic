using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "New ProjectSOInstaller", menuName = "Installers/New ProjectDataInstaller")]
public class ProjectDataInstaller : ScriptableObjectInstaller<ProjectDataInstaller>
{
    [SerializeField] private LevelCatalogSO levelCatalogSO;

    public override void InstallBindings()
    {
        Container.BindInstance(levelCatalogSO).AsSingle().NonLazy();
    }
}