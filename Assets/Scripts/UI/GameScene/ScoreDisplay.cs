using TMPro;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(TMP_Text))]
public class ScoreDisplay : MonoBehaviour
{
    [Inject] private SignalBus signalBus;
    [Inject] private ScoreHandler scoreHandler;

    [SerializeField] private TMP_Text scoreText;

    private void Awake()
    {
        scoreText = GetComponent<TMP_Text>();
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
        scoreText.text = scoreHandler.CurrentScore.ToString();
    }

    public void UpdateScoreText(ScoreChangedSignal signal)
    {
        scoreText.text = signal.NewScore.ToString();
    }

}
