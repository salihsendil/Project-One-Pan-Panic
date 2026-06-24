using TMPro;
using UnityEngine;
using Zenject;
using DG.Tweening;

public class CountdownDisplay : MonoBehaviour
{
    [Inject] private SignalBus signalBus;

    [SerializeField] private TMP_Text countdownText;

    private void Awake()
    {
        if (countdownText == null) { TryGetComponent(out countdownText); }
        Hide();
    }

    private void OnEnable()
    {
        signalBus.Subscribe<CountdownStartedSignal>(Show);
        signalBus.Subscribe<CountdownTickSignal>(UpdateCountdownText);
        signalBus.Subscribe<GameStartedSignal>(Hide);
        signalBus.Subscribe<GameFinishedSignal>(OnGameFinished);
    }

    private void OnDisable()
    {
        signalBus.Unsubscribe<CountdownStartedSignal>(Show);
        signalBus.Unsubscribe<CountdownTickSignal>(UpdateCountdownText);
        signalBus.Unsubscribe<GameStartedSignal>(Hide);
        signalBus.Unsubscribe<GameFinishedSignal>(OnGameFinished);
    }

    private void Hide() => countdownText.gameObject.SetActive(false);
    private void Show() => countdownText.gameObject.SetActive(true);

    private void UpdateCountdownText(CountdownTickSignal signal)
    {
        countdownText.enabled = true;
        countdownText.text = signal.Remaining <= 0 ? "GO!" : signal.Remaining.ToString();
        DoPunchTween();
    }

    private void OnGameFinished()
    {
        Show();
        countdownText.text = "Time's Up!";
        DoPunchTween();
    }

    private void DoPunchTween()
    {
        RectTransform rectTransform = countdownText.rectTransform;
        rectTransform?.DOKill();
        rectTransform.localScale = Vector3.one;
        rectTransform.DOPunchScale(Vector3.one * 0.7f, 0.5f, 5, 0.5f)
            .SetLink(countdownText.gameObject);
    }
}
