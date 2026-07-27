using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using Zenject;

public class SaveSystem : IInitializable
{
    //File Path
#if UNITY_EDITOR
    private string savePath = Application.dataPath;
#else
    private string savePath = Application.persistentDataPath;
#endif

    //Mapping
    public Dictionary<SaveDataType, object> dataMapping = new();

    //Data Models
    private SettingsDataSave settingsData = new();
    private PlayerDataSave playerData = new();
    private LevelSaveRoot levelData = new();

    #region Initialize

    public void Initialize()
    {
        InitializeDataMapping();
        LoadData();
    }

    private void InitializeDataMapping()
    {
        dataMapping[SaveDataType.Settings] = settingsData;
        dataMapping[SaveDataType.PlayerData] = playerData;
        dataMapping[SaveDataType.LevelData] = levelData;
    }

    #endregion

    #region Update

    public T TryGetData<T>(SaveDataType type) where T : class
    {
        if (dataMapping.TryGetValue(type, out object value))
        {
            return value as T;
        }
        return null;
    }

    public void UpdateData<T>(SaveDataType type, T newData) where T : class
    {
        if (dataMapping.ContainsKey(type))
        {
            dataMapping[type] = newData;
            return;
        }

        dataMapping.Add(type, newData);
    }

    #endregion

    #region Save

    public void SaveData(SaveDataType type)
    {
        string filePath = Path.Combine(savePath, type.ToString() + ".json");
        string json = JsonUtility.ToJson(dataMapping[type], true);
        File.WriteAllText(filePath, json);

#if UNITY_EDITOR
        AssetDatabase.Refresh();
#endif
    }

    public void AllSaveData()
    {
        foreach (var type in dataMapping.Keys)
        {
            SaveData(type);
        }
    }

    #endregion

    #region Load

    private void LoadData()
    {
        foreach (var type in dataMapping.Keys)
        {
            string filePath = Path.Combine(savePath, type.ToString() + ".json");

            if (!File.Exists(filePath)) continue;

            string json = File.ReadAllText(filePath);
            object obj =  dataMapping[type];
            JsonUtility.FromJsonOverwrite(json, obj);
        }
    }

    #endregion
}