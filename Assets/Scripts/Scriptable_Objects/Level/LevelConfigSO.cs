using UnityEngine;

[CreateAssetMenu(fileName = "New Level Config", menuName = "Scriptable Objects/Config/New Level Config SO")]
public class LevelConfigSO : ScriptableObject
{
    [Header("Gameplay")]
    public GameplayPhase StartPhase;

    [Header("Time")]
    public int LevelTime;
    public int CountdownTime = 3;

    [Header("Score")]
    public int StartScore = 0;
    public float ScoreMultiplierStepAmount = 0.04f;

    [Header("Currency")]
    public float ScoreCurrencyMultiplier = 0.08f;
}
