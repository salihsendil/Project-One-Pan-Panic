using System;
using System.Collections.Generic;

public enum SaveDataType { Settings = 0, PlayerData = 10, LevelData = 30 }

#region Settings

[Serializable]
public class SettingsDataSave
{
    public float MusicVolume = 0.5f;
    public float SfxVolume = 0.5f;
}

#endregion

#region Player

[Serializable]
public class PlayerDataSave
{
    public int Currency = 0;
    public List<OutfitData> Outfits = new();
}

[Serializable]
public class OutfitData
{
    public BodyPartType Key;
    public string EquippedItem;
    public List<string> OwnedItems = new();

    public OutfitData(BodyPartType key)
    {
        Key = key;
    }
}

#endregion

#region LevelData

[Serializable]
public class LevelSaveRoot
{
    public List<LevelSave> LevelsData = new();
}

[Serializable]
public class LevelSave
{
    public string Level;
    public int HighScore = 0;
    public bool IsLocked = true;
}

#endregion

