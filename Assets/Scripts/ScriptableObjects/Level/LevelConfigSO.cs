using UnityEngine;

[CreateAssetMenu(fileName = "New Level Config", menuName = "Scriptable Objects/New LevelConfigSO")]
public class LevelConfigSO : ScriptableObject
{
    [Header("Level Time")]
    public int levelTime;
}
