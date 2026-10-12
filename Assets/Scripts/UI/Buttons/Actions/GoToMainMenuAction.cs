using UnityEngine;

[RequireComponent(typeof(UIButtonHandler))]
public class GoToMainMenuAction : BaseUIButtonAction
{
    [SerializeField] private MainMenuUIManager uiManager;
    public override void Execute()
    {
        uiManager.GoToMainMenu();
    }
}
