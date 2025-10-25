using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;


[System.Serializable]
public class OwnedItemsDataWrapper
{
    public List<OwnedItemsData> ownedItems = new();
}

[System.Serializable]
public class AppearanceDataWrapper
{
    public List<AppearanceData> appearanceDataList = new();
}


public class CustomizationSaveManager : MonoBehaviour
{
    #region Variables

    //Save File Path
    private string ownedItemsDataSavePath;
    private string customizationDataSavePath;

    //Data Save Wrapper
    private AppearanceDataWrapper appearanceDataWrapper;

    #endregion

    private CustomizationEventQueue events = new();

    //Events!
    public event Action<OwnedItemsDataWrapper> OnOwnedItemDataLoaded;
    public event Action<List<AppearanceData>> OnAppearanceDataLoaded;
    public event Action OnAllDataLoaded;

    private void Start() // If processing is not working on start(not awake), methods couldn’t be subscribers to events
    {
        ownedItemsDataSavePath = Path.Combine(Application.persistentDataPath + "/characterOwnedItemsData.txt");
        customizationDataSavePath = Path.Combine(Application.persistentDataPath + "/characterAppearanceData.txt");

        #region IF_UNITY_EDITOR

#if UNITY_EDITOR
        ownedItemsDataSavePath = Path.Combine(Application.dataPath + "/characterOwnedItemsData.txt");
        customizationDataSavePath = Path.Combine(Application.dataPath + "/characterAppearanceData.txt");
#endif

        #endregion

        events.Enqueue(LoadOwnedItemsData);
        events.Enqueue(LoadAppearanceData);
        events.Enqueue(AllDataLoaded);
        events.Start();
    }

    public void LoadOwnedItemsData(Action onComplete = null)
    {
        OwnedItemsDataWrapper dataWrapper = new();

        try
        {
            if (File.Exists(ownedItemsDataSavePath))
            {
                string jsonInput = File.ReadAllText(ownedItemsDataSavePath);
                dataWrapper = JsonUtility.FromJson<OwnedItemsDataWrapper>(jsonInput);
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to load owned items data: {e.Message}");
        }

        OnOwnedItemDataLoaded?.Invoke(dataWrapper);
        onComplete?.Invoke();
    }

    public void SaveOwnedItemsData(Dictionary<CharacterPartType, HashSet<string>> ownedItems)
    {
        OwnedItemsDataWrapper dataWrapper = new();

        foreach (var data in ownedItems)
        {
            OwnedItemsData ownedData = new(data.Key, data.Value.ToList());
            dataWrapper.ownedItems.Add(ownedData);
        }

        string jsonOutput = JsonUtility.ToJson(dataWrapper, true);
        File.WriteAllText(ownedItemsDataSavePath, jsonOutput);

        Debug.Log($"Data succesfully saved to: {ownedItemsDataSavePath}");
    }

    public void LoadAppearanceData(Action onComplete = null)
    {
        List<AppearanceData> appearances = new();

        try
        {
            if (File.Exists(customizationDataSavePath))
            {
                string jsonInput = File.ReadAllText(customizationDataSavePath);
                AppearanceDataWrapper dataWrapper = JsonUtility.FromJson<AppearanceDataWrapper>(jsonInput);
                appearances = dataWrapper.appearanceDataList;
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to load appearance data: {e.Message}");
        }

        OnAppearanceDataLoaded?.Invoke(appearances);
        onComplete?.Invoke();
    }

    public void SaveAppearanceData(List<AppearanceData> appearances) // <T> List<T> data
    {
        if (appearanceDataWrapper == null) { appearanceDataWrapper = new(); }

        appearanceDataWrapper.appearanceDataList = appearances;

        string jsonOutput = JsonUtility.ToJson(appearanceDataWrapper, true);
        File.WriteAllText(customizationDataSavePath, jsonOutput);

        Debug.Log($"Data succesfully saved to: {customizationDataSavePath}");
    }

    private void AllDataLoaded(Action onComplete = null)
    {
        OnAllDataLoaded?.Invoke();
        onComplete?.Invoke();
    }
}
