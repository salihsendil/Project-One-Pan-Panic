using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New LevelCatalogSO", menuName = "Scriptable Objects/New LevelCatalogSO")]
public class LevelCatalogSO : ScriptableObject
{
    public List<LevelMeta> Levels = new();
}

[Serializable]
public class LevelMeta
{
    [Header("Level Info")]
    public ScenesEnum Scene;
    public string LevelIndex;
    public string LevelName;

    [Header("UI & State")]
    public GameObject FeaturedObject;
    public bool IsLocked;
    public int Highscore;

    public void LoadData(LevelProgress save)
    {
        Highscore = save.HighScore;
        IsLocked = save.IsLocked;
    }

    public void UpdateHighScore(int newValue)
    {
        Highscore = newValue;
    }

    public void UnlockLevel()
    {
        IsLocked = false;
    }
}