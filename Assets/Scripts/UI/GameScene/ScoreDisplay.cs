using TMPro;
using UnityEngine;
using Zenject;

public class ScoreDisplay : MonoBehaviour
{
    [Inject] private SignalBus signalBus;
    [Inject] private ScoreHandler scoreHandler;

    [SerializeField] private TMP_Text scoreText;

    private void Awake()
    {
        if (scoreText == null) { TryGetComponent(out scoreText); }
    }

    private void OnEnable()
    {
        signalBus.Subscribe<ScoreChangedSignal>(UpdateScoreText);
    }

    private void OnDisable()
    {
        signalBus.Unsubscribe<ScoreChangedSignal>(UpdateScoreText);
    }

    private void Start()
    {
        scoreText.text = scoreHandler.GetScore().ToString();
    }

    public void UpdateScoreText(ScoreChangedSignal signal)
    {
        scoreText.text = signal.NewScore.ToString();
    }

}
