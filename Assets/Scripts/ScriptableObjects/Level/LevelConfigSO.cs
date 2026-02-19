using UnityEngine;

[CreateAssetMenu(fileName = "New Level Config", menuName = "Scriptable Objects/New LevelConfigSO")]
public class LevelConfigSO : ScriptableObject
{
    [Header("Countdown Time")]
    public int CountdownTime = 3;

    [Header("Level Time")]
    public int LevelTime;

    [Header("Score")]
    public int StartScore = 0;
}
