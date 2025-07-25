using System;
using UnityEngine.UI;

public interface IStationTimerDisplayer
{
    public void ShowTimer();

    public void SetTimer(float time);

    public void UpdateTimerSlider(float time);

    public void DisableTimer();
}
