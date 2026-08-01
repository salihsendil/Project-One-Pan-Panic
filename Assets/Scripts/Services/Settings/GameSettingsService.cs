using DG.Tweening;
using UnityEngine;
using Zenject;

public class GameSettingsService : IInitializable, ISaveable
{
    #region Const Mixer Group Parameters
    private const string MasterVolume = "Master_Vol";
    private const string MusicVolume = "Music_Vol";
    private const string AmbienceVolume = "Ambience_Vol";
    private const string SFXVolume = "SFX_Vol";
    #endregion

    //References
    [Inject] private SaveSystem saveSystem;

    //Zenject-Data
    [Inject] private GameSettingsSO settingsSO;

    public SaveDataType GetSaveDataType => SaveDataType.Settings;

    public void Initialize()
    {
        LoadData();
        SetMixerDesibels();
    }

    private void SetMixerDesibels()
    {
        float masterVol = LinearToDecibel(settingsSO.MasterVolume);
        settingsSO.MainMixer.SetFloat(MasterVolume, masterVol);

        float musicVol = LinearToDecibel(settingsSO.MusicVolume);
        settingsSO.MainMixer.SetFloat(MusicVolume, musicVol);

        float ambienceVol = LinearToDecibel(settingsSO.AmbienceVolume);
        settingsSO.MainMixer.SetFloat(AmbienceVolume, ambienceVol);

        float sfxVol = LinearToDecibel(settingsSO.SFXVolume);
        settingsSO.MainMixer.SetFloat(SFXVolume, sfxVol);
    }

    private float LinearToDecibel(float volume)
    {
        float curvedValue = Mathf.Pow(volume, 2f);
        float safeLevel = Mathf.Max(curvedValue, settingsSO.MinVolume);

        return Mathf.Log10(safeLevel) * 20f;
    }

    public void UpdateVolume(AudioType audioType, float value)
    {
        switch (audioType)
        {
            case AudioType.Master:
                settingsSO.MasterVolume = value;
                float masterVol = LinearToDecibel(value);
                settingsSO.MainMixer.SetFloat(MasterVolume, masterVol);
                break;

            case AudioType.Music:
                settingsSO.MusicVolume = value;
                float musicVol = LinearToDecibel(value);
                settingsSO.MainMixer.SetFloat(MusicVolume, musicVol);
                break;

            case AudioType.Ambience:
                settingsSO.AmbienceVolume = value;
                float ambienceVol = LinearToDecibel(value);
                settingsSO.MainMixer.SetFloat(AmbienceVolume, ambienceVol);
                break;

            case AudioType.SFX:
                settingsSO.SFXVolume = value;
                float sfxVol = LinearToDecibel(value);
                settingsSO.MainMixer.SetFloat(SFXVolume, sfxVol);
                break;

            default:
                break;
        }
    }

    public float GetVolume(AudioType type)
    {
        return type switch
        {
            AudioType.Master => settingsSO.MasterVolume,
            AudioType.Music => settingsSO.MusicVolume,
            AudioType.Ambience => settingsSO.AmbienceVolume,
            AudioType.SFX => settingsSO.SFXVolume,
            _ => settingsSO.MinVolume,
        };
    }

    #region Save Load System

    public void SaveData()
    {
        SettingsDataSave save = new();
        save.MasterVolume = settingsSO.MasterVolume;
        save.MusicVolume = settingsSO.MusicVolume;
        save.AmbienceVolume = settingsSO.AmbienceVolume;
        save.SfxVolume = settingsSO.SFXVolume;

        saveSystem.UpdateData(GetSaveDataType, save);
        saveSystem.SaveData(GetSaveDataType);
    }

    public void LoadData()
    {
        SettingsDataSave save = saveSystem.TryGetData<SettingsDataSave>(GetSaveDataType);

        if (save==null)
        {
            Debug.Log("boþ");
        }

        settingsSO.MasterVolume = save.MasterVolume;
        settingsSO.MusicVolume = save.MusicVolume;
        settingsSO.AmbienceVolume = save.AmbienceVolume;
        settingsSO.SFXVolume = save.SfxVolume;
    }

    #endregion
}
