using UnityEngine;

[RequireComponent(typeof(UIButtonHandler))]
public class GoToCustomizationAction : BaseUIButtonAction
{
    [SerializeField] private MainMenuUIManager uiManager;
    public override void Execute()
    {
        uiManager.GoToCustomize();
    }
}
