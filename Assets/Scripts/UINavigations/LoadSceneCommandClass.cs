using UnityEngine;
using Zenject;

public class LoadSceneCommandClass : IUICommand<LoadSceneCommandSO>
{
    [Inject] private ISceneService sceneService;

    public void Execute(UICommandSO parameter)
    {
        ExecuteCommand((LoadSceneCommandSO)parameter);
    }

    public void ExecuteCommand(LoadSceneCommandSO parameter)
    {
        sceneService.LoadScene(parameter.sceneType.ToString());
    }
}
