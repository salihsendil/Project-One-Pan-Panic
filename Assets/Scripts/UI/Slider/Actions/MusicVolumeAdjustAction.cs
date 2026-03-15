using UnityEngine;
using Zenject;

[RequireComponent(typeof(UISliderHandler))]
public class MusicVolumeAdjustAction : BaseUIAction<float>
{
    [Inject] private GameDataService gameDataService;

    public override void Execute(float param)
    {
        gameDataService.UpdateMusicVolume(param);
    }
}