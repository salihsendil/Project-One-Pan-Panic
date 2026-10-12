using DG.Tweening;
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

    [Header("Panel")]
    [SerializeField] private CanvasGroup panel;


    [Header("Panel")]
    [SerializeField] private int openDelay = 1000;

    [Header("Texts")]
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
        panel.interactable = false;

        scoreText.alpha = 0;
        scoreText.rectTransform.localScale = Vector3.zero;

        highscoreText.alpha = 0;
        highscoreText.rectTransform.localScale = Vector3.zero;

        failOrderText.alpha = 0;
        failOrderText.rectTransform.localScale = Vector3.zero;

        completeOrderText.alpha = 0;
        completeOrderText.rectTransform.localScale = Vector3.zero;

        earnedGoldText.alpha = 0;
        earnedGoldText.rectTransform.localScale = Vector3.zero;

    }

    private void OnEnable()
    {
        //signalBus.Subscribe<GamePhaseChangedSignal>(ShowSetPanel);
    }

    private void OnDisable()
    {
        //signalBus.Subscribe<GamePhaseChangedSignal>(ShowSetPanel);
    }

    private Sequence AnimateText(TMP_Text text, int targetValue)
    {
        Sequence sequence = DOTween.Sequence();

        sequence.Append(text.DOFade(1f, 0.2f));
        sequence.Join(text.rectTransform.DOScale(Vector3.one * 1.8f, 1f).SetEase(Ease.OutCubic));
        sequence.Join(text.rectTransform.DOLocalRotate(new Vector3(0f, 0f, 15f), 1f).SetEase(Ease.OutCubic));
        sequence.Join(DOVirtual.Int(0, targetValue, 1f, (currentValue) =>
        {
            text.text = currentValue.ToString("N0");
        }).SetEase(Ease.OutQuad));

        sequence.AppendInterval(0.1f);

        sequence.Append(text.rectTransform.DOLocalRotate(new Vector3(0f, 0f, 0f), 0.1f).SetEase(Ease.InExpo));

        sequence.AppendInterval(0.1f);
        sequence.Append(text.rectTransform.DOScale(Vector3.one, 0.1f).SetEase(Ease.InExpo));
        sequence.Append(text.rectTransform.DOShakePosition(0.2f, 15f, 20, 1f));

        return sequence;
    }

    private void SetStatsText()
    {
        Sequence mainSequence = DOTween.Sequence();

        mainSequence.Append(AnimateText(scoreText, levelStatsService.GetStat(StatsType.Score)));
        mainSequence.Append(AnimateText(highscoreText, levelDataService.GetCurrentLevelHighscore()));
        mainSequence.Append(AnimateText(failOrderText, levelStatsService.GetStat(StatsType.FailOrder)));
        mainSequence.Append(AnimateText(completeOrderText, levelStatsService.GetStat(StatsType.CompleteOrder)));
        mainSequence.Append(AnimateText(earnedGoldText, levelStatsService.GetStat(StatsType.EarnedGold)));
    }

    private async void ShowSetPanel()
    {
        await Task.Delay(openDelay);
        
        panel.alpha = 1;
        panel.blocksRaycasts = true;
        panel.interactable = true;

        SetStatsText();
    }
}
