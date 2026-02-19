using TMPro;
using UnityEngine;
using Zenject;

public class CountdownDisplay : MonoBehaviour
{
    [Inject] private SignalBus signalBus;

    [SerializeField] private TMP_Text countdownText;

    private void Awake()
    {
        if (countdownText == null) { TryGetComponent(out countdownText); }
    }

    private void OnEnable()
    {
        signalBus.Subscribe<CountdownTickSignal>(UpdateCountdownText);
    }

    private void OnDisable()
    {
        signalBus.Unsubscribe<CountdownTickSignal>(UpdateCountdownText);
    }

    private void UpdateCountdownText(CountdownTickSignal signal)
    {
        countdownText.text = signal.Remaining <= 0 ? "GO!" : signal.Remaining.ToString();
    }

    public void OnGameStarted()
    {
        gameObject.SetActive(false);
    }

    public void OnGameFinished()
    {
        gameObject.SetActive(true);
        countdownText.text = "Time's Up!";
    }
}
