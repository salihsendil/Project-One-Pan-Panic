using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "New Project_SO_Installer", menuName = "Installers/New Project_SO_Installer")]
public class Project_Data_Installer : ScriptableObjectInstaller<Project_Data_Installer>
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