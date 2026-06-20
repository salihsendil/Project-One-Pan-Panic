using UnityEngine;

[RequireComponent(typeof(UIButtonHandler))]
public class GoToMainMenuAction : BaseUIAction
{
    [SerializeField] private MainMenuUIManager uiManager;
    public override void Execute()
    {
        uiManager.GoToMainMenu();
    }
}
