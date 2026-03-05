using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Newtonsoft.Json;

public class SaveSystem
{
    private HashSet<ISaveable> iSaveables = new();

    public void Register(ISaveable saveable)
    {
        iSaveables.Add(saveable);
    }

    public void Unregister(ISaveable saveable)
    {
        iSaveables.Remove(saveable);
    }

    public void SaveData()
    {

        SaveFile saveFile = new();

        foreach (var saveable in iSaveables)
        {
            SaveFileEntry entry = new();
            entry.SaveDataType = saveable.GetSaveDataType();
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

        var data = JsonConvert.DeserializeObject<SaveFile>(json);

        foreach (var saveable in iSaveables)
        {
            string jsonData = data.Entries.Find(x => x.SaveDataType == saveable.GetSaveDataType()).JsonData;

            if (jsonData == null) { continue; }

            saveable.LoadData(jsonData);
        }
    }
}

