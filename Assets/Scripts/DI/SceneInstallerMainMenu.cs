using UnityEngine;
using Zenject;

public class SceneInstallerMainMenu : MonoInstaller
{

    public override void InstallBindings()
    {
        foreach (var buttonAction in FindObjectsByType<UIButtonAction>(FindObjectsSortMode.None))
        {
            Container.Inject(buttonAction);
        }


        //will be update
        Container.Bind<CustomizationSaveManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<CustomizationDataProvider>().FromComponentInHierarchy().AsSingle();
        Container.Bind<CustomizationUIController>().FromComponentInHierarchy().AsSingle();
        Container.Bind<AppearanceManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<OwnedItemsManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<CurrencyManager>().FromComponentInHierarchy().AsSingle();
    }
}
