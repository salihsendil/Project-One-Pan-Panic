using UnityEngine;
using Zenject;

[RequireComponent(typeof(UISliderHandler))]
public class SoundVolumeAdjustAction : BaseUIAction<float>
{
    [Inject] private GameDataService settingsService;

    public override void Execute(float param)
    {
        settingsService.UpdateSfxVolume(param);
    }
}