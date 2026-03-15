using Newtonsoft.Json;
using System;
using Zenject;

[Serializable]
public struct GameSettingsData
{
    public float MusicVolume;
    public float SfxVolume;
}

public class GameSettingsService : IInitializable, IDisposable, ISaveable
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
        GameSettingsData settingsData = new();
        settingsData.MusicVolume = musicVolume;
        settingsData.SfxVolume = sfxVolume;
        return JsonConvert.SerializeObject(settingsData, Formatting.Indented);
    }

    public void LoadData(string json)
    {
        GameSettingsData settingsData = JsonConvert.DeserializeObject<GameSettingsData>(json);
        musicVolume = settingsData.MusicVolume;
        sfxVolume = settingsData.SfxVolume;
    }
}
