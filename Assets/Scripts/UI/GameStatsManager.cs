using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameStatsManager : MonoBehaviour
{
    [Inject] private GameManager gameManager;

    [Header("Timer")]
    [SerializeField] private float time;
    public event Action<float> OnTimerValueChanged;

    [Header("Score")]
    [SerializeField] private int score;
    public event Action<int> OnScoreValueChanged;

    private void Awake()
    {
        time = gameManager.GameConfig.GameTime;
        score = gameManager.GameConfig.StartScore;
    }

    private void Update()
    {
        time -= Time.deltaTime;
        OnTimerValueChanged?.Invoke(time);
    }

    public void UpdateScore(int addScore)
    {
        score += addScore;
        OnScoreValueChanged?.Invoke(score);
    }

}
