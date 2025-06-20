using TMPro;
using UnityEngine;
using Zenject;

public class TimerDisplay : MonoBehaviour
{
    [Inject] private GameStatsManager gameStatsManager;

    [Header("Timer")]
    [SerializeField] private TMP_Text timerText;

    void Start()
    {
        gameStatsManager.OnTimerValueChanged += UpdateTimer;
    }

    private void UpdateTimer(float newTime)
    {
        float time = Mathf.Max(0, newTime);
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        timerText.SetText($"{minutes:D2}:{seconds:D2}");
    }

}
