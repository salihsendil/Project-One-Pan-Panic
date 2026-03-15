using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(UISliderHandler))]
public class SFXVolumeAdjustAction : BaseUIAction<float>
{
    [Inject] private GameSettingsService settingsService;
    private UISliderHandler sliderHandler;

    public override void Execute(float param)
    {
        settingsService.UpdateSfxVolume(param);
    }

    private void Start()
    {
        sliderHandler = GetComponent<UISliderHandler>();
        if (sliderHandler == null)
        {
            Debug.Log("boþ" +
                "");
        }
        sliderHandler.SetSliderValue(settingsService.SfxVolume);
    }
}