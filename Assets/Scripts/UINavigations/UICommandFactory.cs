using System.Collections.Generic;
using Zenject;

public class UICommandFactory
{
    public Dictionary<UICommandType, IUICommand> commandMap = new();

    [Inject] private LoadSceneCommand LoadSceneCommand;
    [Inject] private PopupCommand PopupCommand;

    public void Initialize()
    {
        commandMap.Add(UICommandType.LoadScene, LoadSceneCommand);
        commandMap.Add(UICommandType.Popup, PopupCommand);
    }

}
