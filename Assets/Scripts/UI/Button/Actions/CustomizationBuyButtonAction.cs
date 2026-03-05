using UnityEngine;

[RequireComponent(typeof(UIButtonHandler))]
public class CustomizationBuyButtonAction : BaseUIAction
{
    [SerializeField] private CustomizationManager customizationManager;
    public override void Execute()
    {
        customizationManager.HandleBuyButton();
    }
}
