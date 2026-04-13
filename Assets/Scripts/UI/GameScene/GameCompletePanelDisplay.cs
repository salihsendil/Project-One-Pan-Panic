using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(CanvasGroup))]
public class GameCompletePanelDisplay : MonoBehaviour
{
    //Zenject
    [Inject] private SignalBus signalBus;
    [Inject] private LevelStatsService statsService;
    [Inject] private ScoreHandler scoreHandler;

    //References
    private CanvasGroup panel;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text highscoreText;
    [SerializeField] private TMP_Text failOrderText;
    [SerializeField] private TMP_Text completeOrderText;
    [SerializeField] private TMP_Text earnedGoldText;

    private void Awake()
    {
        panel = GetComponent<CanvasGroup>();
        panel.alpha = 0;
        panel.blocksRaycasts = false;
    }

    private void OnEnable()
    {
        signalBus.Subscribe<GameFinishedSignal>(HandleGameFinished);
    }

    private void OnDisable()
    {
        signalBus.Unsubscribe<GameFinishedSignal>(HandleGameFinished);
    }

    private async void HandleGameFinished()
    {
        await Task.Delay(2000);
        SetStatsText();
        panel.alpha = 1;
        panel.blocksRaycasts = true;
    }

    private void SetStatsText()
    {
        scoreText.text = "Score: " + statsService.GetStat(StatsType.Score).ToString();
        highscoreText.text = "High Score: " + scoreHandler.HighScore.ToString();
        failOrderText.text = "Failed Order: " + statsService.GetStat(StatsType.FailOrder).ToString();
        completeOrderText.text = "Completed Order: " + statsService.GetStat(StatsType.CompleteOrder).ToString();
        earnedGoldText.text = "Earned Gold: " + statsService.GetStat(StatsType.EarnedGold).ToString();
    }

}
