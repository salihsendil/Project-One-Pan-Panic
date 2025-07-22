using UnityEngine;

public class PopupCommandClass : IUICommand<UICommandSO>
{
    public GameObject currentPopupPanel;

    public void Execute(UICommandSO parameter)
    {
        ExecuteCommand(parameter);
    }

    public void ExecuteCommand(UICommandSO parameter)
    {
        if (currentPopupPanel != null)
        {
            currentPopupPanel.SetActive(parameter);
        }
    }
}
