using UnityEngine;
using TMPro;
using Zenject;

public class ScoreDisplay : MonoBehaviour
{
    [Inject] private GameStatsManager gameStatsManager;

    [Header("Score")]
    [SerializeField] private TMP_Text scoreText;

    private void OnEnable()
    {
        gameStatsManager.OnScoreValueChanged += UpdateScoreText;
    }

    private void OnDisable()
    {
        gameStatsManager.OnScoreValueChanged -= UpdateScoreText;
    }

    private void UpdateScoreText(int score)
    {
        scoreText.SetText($"{score}");
    }

}
