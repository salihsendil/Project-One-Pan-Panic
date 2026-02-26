using UnityEngine;

[RequireComponent(typeof(UISliderHandler))]
public class SoundVolumeAdjustAction : BaseUIAction<float>
{
    public override void Execute(float param)
    {
        Debug.Log("current sound level is: " + param);
    }
}