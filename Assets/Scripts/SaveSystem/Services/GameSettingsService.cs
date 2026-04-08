using Zenject;

public class GameSettingsService : ISaveable, IInitializable
{
    //Zenject
    [Inject] private SaveSystem saveSystem;

    //Volume Variables
    private float musicVolume = 0.5f;
    private float sfxVolume = 0.5f;

    //Properties
    public SaveDataType GetSaveDataType => SaveDataType.Settings;
    public float MusicVolume  => musicVolume; 
    public float SfxVolume  => sfxVolume; 


    public void Initialize()
    {
        LoadData();
    }

    public void UpdateMusicVolume(float newValue)
    {
        musicVolume = newValue;
        SaveData();
    }

    public void UpdateSfxVolume(float newValue)
    {
        sfxVolume = newValue;
        SaveData();
    }

    #region SaveLoadData

    public void SaveData()
    {
        SettingsDataSave newData = new();
        newData.MusicVolume = musicVolume;
        newData.SfxVolume = sfxVolume;
        saveSystem.UpdateData(GetSaveDataType, newData);
        saveSystem.SaveData(GetSaveDataType);
    }

    public void LoadData()
    {
        SettingsDataSave data = saveSystem.GetData<SettingsDataSave>(GetSaveDataType);
        musicVolume = data.MusicVolume;
        sfxVolume = data.SfxVolume;
    }

    #endregion
}
