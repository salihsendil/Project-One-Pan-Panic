using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "New ProjectSOInstaller", menuName = "Installers/New ProjectDataInstaller")]
public class ProjectDataInstaller : ScriptableObjectInstaller<ProjectDataInstaller>
{
    [SerializeField] private SFXLibrarySO sfxLibrarySO;
    [SerializeField] private LevelCatalogSO levelCatalogSO;
    [SerializeField] private GameSettingsSO gameSettingsSO;

    public override void InstallBindings()
    {
        Container.BindInstance(sfxLibrarySO).AsSingle().NonLazy();
        Container.BindInstance(levelCatalogSO).AsSingle().NonLazy();
        Container.BindInstance(gameSettingsSO).AsSingle().NonLazy();
    }
}