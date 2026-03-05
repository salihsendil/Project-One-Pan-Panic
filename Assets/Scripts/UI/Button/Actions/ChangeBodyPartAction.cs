using UnityEngine;

[RequireComponent(typeof(UIButtonHandler))]
public class ChangeBodyPartAction : BaseUIAction
{
    [SerializeField] private CustomizationManager customizationManager;
    [SerializeField] private int stepSize;

    public override void Execute()
    {
        customizationManager.OnChangeBodyPart(stepSize);
    }
}
