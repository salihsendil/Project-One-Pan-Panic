using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        SignalInstallers();
    }

    private void SignalInstallers()
    {
        SignalBusInstaller.Install(Container);
    
        
    }
}