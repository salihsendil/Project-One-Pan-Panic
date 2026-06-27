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
    public ScenesEnum Level;
    public Sprite LevelFeaturedImage;
    public string LevelIndex;
    public string LevelName;
    public bool IsLocked;
    public int Highscore;

    public void LoadData(LevelSave save)
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