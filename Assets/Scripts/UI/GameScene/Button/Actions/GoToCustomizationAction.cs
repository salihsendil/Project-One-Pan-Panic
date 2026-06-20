using UnityEngine;

[RequireComponent(typeof(UIButtonHandler))]
public class GoToCustomizationAction : BaseUIAction
{
    [SerializeField] private MainMenuUIManager uiManager;
    public override void Execute()
    {
        uiManager.GoToCustomize();
    }
}
