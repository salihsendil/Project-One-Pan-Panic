using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class StationTimerDisplay : MonoBehaviour, IStationTimerDisplayer
{
    [Inject] private GameManager gameManager;
    [Inject] private BillboardManager billboardManager;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Image timerBackGround;
    [SerializeField] private Image timerImage;
    private float duration;

    private void Awake()
    {
        timerBackGround.color = Color.clear;
        timerImage.color = Color.clear;
        billboardManager.CanvasLookAtCamera(canvas);
    }

    public void ShowTimer()
    {
        timerBackGround.color = Color.white;
        timerImage.color = gameManager.GameConfig.stationTimerUIColor;
        timerImage.fillAmount = 0;

    }

    public void SetTimer(float time)
    {
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
