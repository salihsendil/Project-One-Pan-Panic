using UnityEngine;

[RequireComponent(typeof(UIButtonHandler))]
public class CustomizationBuyButtonAction : BaseUIButtonAction
{
    [SerializeField] private CustomizationManager customizationManager;
    public override void Execute()
    {
        customizationManager.HandleBuyButton();
    }
}
