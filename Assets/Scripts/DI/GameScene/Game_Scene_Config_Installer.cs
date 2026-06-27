using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "New LevelConfigSOInstaller", menuName = "Installers/New LevelConfigSOInstaller")]
public class Game_Scene_Config_Installer : ScriptableObjectInstaller<Game_Scene_Config_Installer>
{
    [SerializeField] private LevelConfigSO levelConfigSO;
    [SerializeField] private OrderConfigSO orderConfigSO;
    [SerializeField] private UniversalPoolConfigSO poolConfigSO;

    public override void InstallBindings()
    {
        Container.BindInstance(levelConfigSO).AsSingle();
        Container.BindInstance(orderConfigSO).AsSingle();
        Container.BindInstance(poolConfigSO).AsSingle();
    }
}