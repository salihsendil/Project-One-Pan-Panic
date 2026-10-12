using UnityEngine;

[CreateAssetMenu(fileName = "New Level Config", menuName = "Scriptable Objects/Level/New Level Config SO")]
public class LevelConfigSO : ScriptableObject
{
    [Header("Gameplay")]
    public GamePhase StartPhase;

    [Header("Time")]
    public int CountdownTime = 3;
    public int LevelTime;

    [Header("Score")]
    public float ComboMultiplierIncrement = 0.04f;

    [Header("Currency")]
    public float ScoreCurrencyMultiplier = 0.08f;
}
