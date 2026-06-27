using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(CanvasGroup))]
public class GameCompletePanelDisplay : MonoBehaviour
{
    //Zenject
    [Inject] private SignalBus signalBus;
    [Inject] private LevelStatsService levelStatsService;
    [Inject] private LevelDataService levelDataService;

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
        scoreText.text = "Score: " + levelStatsService.GetStat(StatsType.Score).ToString();
        highscoreText.text = "High Score: " + levelDataService.GetCurrentLevelHighscore().ToString();
        failOrderText.text = "Failed Order: " + levelStatsService.GetStat(StatsType.FailOrder).ToString();
        completeOrderText.text = "Completed Order: " + levelStatsService.GetStat(StatsType.CompleteOrder).ToString();
        earnedGoldText.text = "Earned Gold: " + levelStatsService.GetStat(StatsType.EarnedGold).ToString();
    }

}
