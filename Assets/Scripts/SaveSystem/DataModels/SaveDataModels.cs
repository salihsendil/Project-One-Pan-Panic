using System;
using System.Collections.Generic;

public enum SaveDataType { Settings = 0, PlayerData = 10, Stats = 20 }

[Serializable]
public class SettingsDataSave
{
    public float MusicVolume;
    public float SfxVolume;
}

[Serializable]
public class PlayerDataSave
{
    public int Currency;
    public List<OutfitData> Outfits = new();
}

[Serializable]
public class OutfitData
{
    public BodyPartType Key;
    public string EquippedItem;
    public List<string> OwnedItems = new();
}

[Serializable]
public class StatsDataSave
{
    public int HighScore;
}


