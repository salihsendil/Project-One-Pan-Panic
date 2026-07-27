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

    //Data-Getter
    public LevelCatalogSO LevelCatalog => levelCatalog;


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
            if (levels[i].Level != currentScene) return;

            Debug.Log("level bulundu: " + currentScene + ", ve indexi:" + i);

            if (levels[i].Highscore <= score)
            {
                levels[i].UpdateHighScore(score);
            }

            if (i + 1 <= levels.Count)
            {
                levels[i + 1].UnlockLevel();
            }

            break;
        }

        SaveData();
    }

    public int GetCurrentLevelHighscore()
    {
        return levelCatalog.Levels.Find(x => x.Level == currentScene).Highscore;
    }

    #region SaveLoad

    public SaveDataType GetSaveDataType => SaveDataType.LevelData;

    public void LoadData()
    {
        if (levelCatalog == null) return;

        LevelSaveRoot saveRoot = saveSystem.TryGetData<LevelSaveRoot>(GetSaveDataType);
        if (saveRoot == null) return;

        List<LevelMeta> levels = levelCatalog.Levels;
        foreach (var data in saveRoot.LevelsData)
        {
            LevelMeta level = levelCatalog.Levels.Find(x => x.Level.ToString() == data.Level);
            if (level == null) continue;
            level.LoadData(data);
        }
    }

    public void SaveData()
    {
        LevelSaveRoot newData = new();

        foreach (var level in levelCatalog.Levels)
        {
            if (level == null) continue;

            LevelSave data = new();
            data.Level = level.Level.ToString();
            data.HighScore = level.Highscore;
            data.IsLocked = level.IsLocked;

            newData.LevelsData.Add(data);
        }

        saveSystem.UpdateData(GetSaveDataType, newData);
        saveSystem.SaveData(GetSaveDataType);
    }

    #endregion
}
