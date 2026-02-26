using UnityEngine;

[RequireComponent(typeof(UISliderHandler))]
public class MusicVolumeAdjustAction : BaseUIAction<float>
{
    public override void Execute(float param)
    {
        Debug.Log("current music level is: " + param);
    }
}