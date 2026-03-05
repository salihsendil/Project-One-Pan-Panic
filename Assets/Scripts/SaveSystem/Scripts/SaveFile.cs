using System;
using System.Collections.Generic;
using Newtonsoft.Json;

public enum SaveDataType { Wardrobe, Currency}

[Serializable]
public class WardrobeSaveData
{
    public BodyPartType Key;
    public string EquippedItem;
    public List<string> OwnedItems = new();
}

[Serializable]
public class SaveFileEntry
{
    public SaveDataType SaveDataType;
    public string JsonData;
}

[Serializable]
public class SaveFile
{
    public List<SaveFileEntry> Entries = new();
}