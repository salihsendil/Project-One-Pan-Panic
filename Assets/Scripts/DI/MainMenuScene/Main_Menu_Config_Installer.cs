using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "New Main_Menu_Config_Installer", menuName = "Installers/New Main_Menu_Config_Installer")]
public class Main_Menu_Config_Installer : ScriptableObjectInstaller<Main_Menu_Config_Installer>
{
    [SerializeField] private AudioProfileSO audioProfileSO;

    public override void InstallBindings()
    {
        Container.BindInstance(audioProfileSO).AsSingle();
    }
}