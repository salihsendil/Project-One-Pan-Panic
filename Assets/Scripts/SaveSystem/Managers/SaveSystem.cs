using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using Newtonsoft.Json;
using Zenject;

public class SaveSystem : IInitializable
{
    private HashSet<ISaveable> iSaveables = new();

    private SaveFile saveFile;

    public void Register(ISaveable saveable)
    {
        iSaveables.Add(saveable);

        foreach (var saveEntry in saveFile.Entries)
        {
            if (saveable.GetSaveDataType == saveEntry.SaveDataType)
            {
                saveable.LoadData(saveEntry.JsonData);
            }
        }
    }

    public void Unregister(ISaveable saveable)
    {
        iSaveables.Remove(saveable);
    }

    //In the future, when we split the log files, we will need to store the data across multiple files.
    //But for now, since we’re keeping all the data in a single JSON file, the system is logging everything.
    public void SaveData()
    {

        SaveFile saveFile = new();

        foreach (var saveable in iSaveables)
        {
            SaveFileEntry entry = new();
            entry.SaveDataType = saveable.GetSaveDataType;
            entry.JsonData = saveable.GetSaveData();
            saveFile.Entries.Add(entry);
        }

        string json = JsonConvert.SerializeObject(saveFile, Formatting.Indented);
        Debug.Log("final correct json: " + json);
        string filePath = Path.Combine(Application.dataPath, "playerSaveTest" + ".json");
        File.WriteAllText(filePath, json);
        AssetDatabase.Refresh();
    }

    public void LoadData()
    {
        string filePath = Path.Combine(Application.dataPath, "playerSaveTest" + ".json");

        if (!File.Exists(filePath)) return;

        string json = File.ReadAllText(filePath);

        saveFile = JsonConvert.DeserializeObject<SaveFile>(json);
        //foreach (var saveable in iSaveables)
        //{
        //    string jsonData = data.Entries.Find(x => x.SaveDataType == saveable.GetSaveDataType).JsonData;

        //    if (jsonData == null) { continue; }

        //    saveable.LoadData(jsonData);
        //}
    }

    public void Initialize()
    {
        LoadData();
    }
}

