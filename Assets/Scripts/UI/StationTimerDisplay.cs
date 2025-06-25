using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class StationTimerDisplay : MonoBehaviour, IStationTimerDisplayer
{
    [Inject] private GameManager gameManager;
    [SerializeField] private Image timerBackGround;
    [SerializeField] private Image timerImage;
    private float duration;

    private void Awake()
    {
        timerBackGround.color = Color.clear;
        timerImage.color = Color.clear;
    }

    public void SetTimer(float time)
    {
        timerBackGround.color = Color.white;
        timerImage.color = gameManager.GameConfig.stationTimerUIColor;
        timerImage.fillAmount = duration = time;
    }

    public void UpdateTimerSlider(float time)
    {
        timerImage.fillAmount = time / duration;
    }

    public void DisableTimer()
    {
        timerBackGround.color = Color.clear;
        timerImage.color = Color.clear;
        duration = 0;
    }
}
