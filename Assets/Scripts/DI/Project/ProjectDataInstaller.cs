using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "New ProjectSOInstaller", menuName = "Installers/New ProjectDataInstaller")]
public class ProjectDataInstaller : ScriptableObjectInstaller<ProjectDataInstaller>
{
    [SerializeField] private AudioConfigSO audioConfigSO;
    [SerializeField] private LevelCatalogSO levelCatalogSO;

    public override void InstallBindings()
    {
        Container.BindInstance(audioConfigSO).AsSingle().NonLazy();
        Container.BindInstance(levelCatalogSO).AsSingle().NonLazy();
    }
}