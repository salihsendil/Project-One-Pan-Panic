using UnityEngine;

[RequireComponent(typeof(UIButtonHandler))]
public class ChangeClothAction : BaseUIAction
{
    [SerializeField] private CustomizationManager customizationManager;
    [SerializeField] private int stepSize;

    public override void Execute()
    {
        customizationManager.OnChangeCloth(stepSize);
    }
}
