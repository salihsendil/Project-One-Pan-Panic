using UnityEngine;

[CreateAssetMenu(fileName = "New Level Config", menuName = "Scriptable Objects/New LevelConfigSO")]
public class LevelConfigSO : ScriptableObject
{
    [Header("Gameplay Settings")]
    public GameplayPhase StartPhase;
    public int LevelTime;

    [Header("Countdown Time")]
    public int CountdownTime = 3;

    [Header("Score")]
    public int StartScore = 0;
    public float ScoreMultiplierStepAmount = 0.04f;

    [Header("Currency")]
    public float ScoreCurrencyMultiplier = 0.08f;
}
