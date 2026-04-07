using System;
using System.Collections.Generic;


#region OldSaveSystem

public enum SaveDataType { Wardrobe = 0, Currency = 10, Highscore = 20, Settings = 40 }

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

#endregion


/*--------------------------------------------*/


#region RefactorSaveSystem

public enum SaveDataTypeTest { Settings = 0, PlayerData = 10, Stats = 20 }

[Serializable]
public class SettingsDataSave
{
    public float MusicVolume;
    public float SfxVolume;
}

[Serializable]
public class PlayerDataSave
{

}

[Serializable]
public class StatsDataSave
{
    public int HighScore;
}

#endregion