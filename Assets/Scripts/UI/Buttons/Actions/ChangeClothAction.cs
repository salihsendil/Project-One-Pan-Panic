using UnityEngine;

[RequireComponent(typeof(UIButtonHandler))]
public class ChangeClothAction : BaseUIButtonAction
{
    [SerializeField] private CustomizationManager customizationManager;
    [SerializeField] private int stepSize;

    public override void Execute()
    {
        customizationManager.ChangeCloth(stepSize);
    }
}
