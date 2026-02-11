using UnityEngine;
using TMPro;
using Zenject;

public class TimerDisplay : MonoBehaviour
{
    [Inject] private SignalBus signalBus;

    [SerializeField] private TMP_Text timerText;

    private void Awake()
    {
        if (timerText == null) { TryGetComponent(out timerText); }
    }

    private void OnEnable()
    {
        signalBus.Subscribe<LevelTimerTickSignal>(UpdateTimerText);
    }

    private void OnDisable()
    {
        signalBus.Unsubscribe<LevelTimerTickSignal>(UpdateTimerText);
    }

    public void UpdateTimerText(LevelTimerTickSignal signal)
    {
        int minute = signal.SecondsLeft / 60;
        int second = signal.SecondsLeft % 60;
        timerText.text = $"{minute:D2}:{second:D2}";
    }
}
