using Newtonsoft.Json;
using System;
using UnityEngine;
using Zenject;

public class GameDataService : IInitializable, IDisposable, ISaveable
{
    [Inject] private SaveSystem saveSystem;

    private float musicVolume = 0.5f;
    private float sfxVolume = 0.5f;

    public SaveDataType GetSaveDataType => SaveDataType.Settings;

    public float MusicVolume { get => musicVolume; }
    public float SfxVolume { get => sfxVolume; }

    public void Initialize()
    {
        saveSystem.Register(this);
    }

    public void Dispose()
    {
        //saveSystem.SaveData();
        saveSystem.Unregister(this);
    }

    public void UpdateMusicVolume(float newValue)
    {
        musicVolume = newValue;
    }

    public void UpdateSfxVolume(float newValue)
    {
        sfxVolume = newValue;
    }

    public string GetSaveData()
    {
        GameData settingsData = new();
        settingsData.MusicVolume = musicVolume;
        settingsData.SfxVolume = sfxVolume;
        return JsonConvert.SerializeObject(settingsData, Formatting.Indented);
    }

    public void LoadData(string json)
    {
        GameData settingsData = JsonConvert.DeserializeObject<GameData>(json);
        musicVolume = settingsData.MusicVolume;
        sfxVolume = settingsData.SfxVolume;
    }
}
