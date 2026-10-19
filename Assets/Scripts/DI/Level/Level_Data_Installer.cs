using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "New Level_SO_Installer", menuName = "Installers/New Level_SO_Installer")]
public class Level_Data_Installer : ScriptableObjectInstaller<Level_Data_Installer>
{
    [SerializeField] private LevelConfigSO levelConfigSO;
    [SerializeField] private OrderConfigSO orderConfigSO;
    [SerializeField] private PoolConfigSO poolConfigSO;
    [SerializeField] private AudioProfileSO audioProfileSO;

    public override void InstallBindings()
    {
        Container.BindInstance(levelConfigSO).AsSingle();
        Container.BindInstance(orderConfigSO).AsSingle();
        Container.BindInstance(poolConfigSO).AsSingle();
        Container.BindInstance(audioProfileSO).AsSingle();
    }
}