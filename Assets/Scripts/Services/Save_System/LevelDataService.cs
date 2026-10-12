using Zenject;
using System.Collections.Generic;
using System;
using UnityEngine;

public class LevelDataService : ISaveable, IInitializable, IDisposable
{
    //Zenject
    [Inject] private SignalBus signalBus;
    [Inject] private SaveSystem saveSystem;
    [Inject] private LevelCatalogSO levelCatalog;

    //Scene
    private ScenesEnum currentScene;

    public void Initialize()
    {
        LoadData();

        signalBus.Subscribe<SceneFullyLoadedSignal>(SceneChanged);
    }

    public void Dispose()
    {
        signalBus.Unsubscribe<SceneFullyLoadedSignal>(SceneChanged);
    }

    private void SceneChanged(SceneFullyLoadedSignal signal)
    {
        currentScene = signal.LoadedScene;
    }

    public void LevelCompleted(int score)
    {
        List<LevelMeta> levels = levelCatalog.Levels;

        for (int i = 0; i < levels.Count; i++)
        {
            if (levels[i].Scene != currentScene)
                continue;

            Debug.Log($"Level bulundu: {currentScene}, index: {i}");

            if (score > levels[i].Highscore)
            {
                levels[i].UpdateHighScore(score);
            }

            if (i + 1 < levels.Count)
            {
                levels[i + 1].UnlockLevel();
            }

            SaveData();
            break;
        }
    }

    public int GetCurrentLevelHighscore()
    {
        return levelCatalog.Levels.Find(x => x.Scene == currentScene).Highscore;
    }

    #region SaveLoad

    public SaveDataType GetSaveDataType => SaveDataType.LevelData;

    public void LoadData()
    {
        if (levelCatalog == null) return;

        LevelSaveDatabase saveDatabase = saveSystem.TryGetData<LevelSaveDatabase>(GetSaveDataType);
        if (saveDatabase == null) return;

        List<LevelMeta> levels = levelCatalog.Levels;
        foreach (var data in saveDatabase.LevelsData)
        {
            LevelMeta level = levelCatalog.Levels.Find(x => x.Scene.ToString() == data.Level);
            if (level == null) continue;
            level.LoadData(data);
        }
    }

    public void SaveData()
    {
        LevelSaveDatabase newData = new();

        foreach (var level in levelCatalog.Levels)
        {
            if (level == null) continue;

            LevelProgress data = new();
            data.Level = level.Scene.ToString();
            data.HighScore = level.Highscore;
            data.IsLocked = level.IsLocked;

            newData.LevelsData.Add(data);
        }

        saveSystem.UpdateData(GetSaveDataType, newData);
        saveSystem.SaveData(GetSaveDataType);
    }

    #endregion
}
