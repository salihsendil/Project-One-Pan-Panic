using DG.Tweening;
using UnityEngine;
using Zenject;

public class AudioService : MonoBehaviour, IInitializable, ISaveable
{
    #region Const Mixer Group Parameters
    private const string MasterVolume = "Master_Vol";
    private const string MusicVolume = "Music_Vol";
    private const string AmbienceVolume = "Ambience_Vol";
    private const string SFXVolume = "SFX_Vol";
    private const string UISFXVolume = "UI_SFX_Vol";
    #endregion

    //Zenject Data
    [Inject] private AudioConfigSO audioConfig;

    //Zenject
    [Inject] private SignalBus signalBus;
    [Inject] private SaveSystem saveSystem;

    //References
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource ambienceSource;
    [SerializeField] private AudioSource sfxSource;

    public SaveDataType GetSaveDataType => SaveDataType.Settings;

    public void Initialize()
    {
        LoadData();
    }

    #region SaveLoadData
    public void SaveData()
    {
        SettingsDataSave data = new();

        data.MasterVolume = audioConfig.MasterVolume;
        data.MusicVolume = audioConfig.MusicVolume;
        data.AmbienceVolume = audioConfig.AmbienceVolume;
        data.SfxVolume = audioConfig.SFXVolume;

        saveSystem.UpdateData(GetSaveDataType, data);
        saveSystem.SaveData(GetSaveDataType);
    }

    public void LoadData()
    {
        SettingsDataSave data = saveSystem.TryGetData<SettingsDataSave>(GetSaveDataType);
        audioConfig.MasterVolume = data.MasterVolume;
        audioConfig.MusicVolume = data.MusicVolume;
        audioConfig.AmbienceVolume = data.AmbienceVolume;
        audioConfig.SFXVolume = data.SfxVolume;
    }

    #endregion

    private void OnEnable()
    {
        signalBus.Subscribe<SceneFullyLoadedSignal>(OnSceneChanged);
        signalBus.Subscribe<ItemTransferredSignal>(PlayItemTransferredSFX);
        signalBus.Subscribe<IngredientAddedToContainerSignal>(PlayItemTransferredSFX);
        signalBus.Subscribe<CountdownStartedSignal>(PlayCountDownStartSFX);
        signalBus.Subscribe<LevelTimerTickSignal>(PlayTenSecBeepSFX);
    }

    private void OnDisable()
    {
        signalBus.Unsubscribe<SceneFullyLoadedSignal>(OnSceneChanged);
        signalBus.Unsubscribe<ItemTransferredSignal>(PlayItemTransferredSFX);
        signalBus.Unsubscribe<IngredientAddedToContainerSignal>(PlayItemTransferredSFX);
        signalBus.Unsubscribe<CountdownStartedSignal>(PlayCountDownStartSFX);
        signalBus.Unsubscribe<LevelTimerTickSignal>(PlayTenSecBeepSFX);
    }

    void Start()
    {
        SetDesibels();
        PlayBackgroundMusic(ScenesEnum.Main_Menu_Scene);
    }

    private void SetDesibels()
    {
        float masterVol = GetLinearToDecibel(audioConfig.MinVolume);
        audioConfig.MainMixer.DOSetFloat(MasterVolume, masterVol, 1.5f);

        float musicVol = GetLinearToDecibel(audioConfig.MusicVolume);
        audioConfig.MainMixer.DOSetFloat(MusicVolume, musicVol, 1.5f);

        float ambienceVol = GetLinearToDecibel(audioConfig.AmbienceVolume);
        audioConfig.MainMixer.DOSetFloat(AmbienceVolume, ambienceVol, 1.5f);

        float sfxVol = GetLinearToDecibel(audioConfig.SFXVolume);
        audioConfig.MainMixer.DOSetFloat(SFXVolume, sfxVol, 1.5f);
        audioConfig.MainMixer.DOSetFloat(UISFXVolume, sfxVol, 1.5f);
    }

    private void OnSceneChanged(SceneFullyLoadedSignal signal) => PlayBackgroundMusic(signal.LoadedScene);

    private void PlayBackgroundMusic(ScenesEnum scene)
    {
        AudioProfile profile = audioConfig.Profiles.Find(x => x.Scene == scene);

        if (profile == null) return;

        musicSource.resource = profile.Music;
        musicSource.loop = true;
        musicSource.Play();

        if (profile.Ambience != null)
        {
            ambienceSource.resource = profile.Ambience;
            ambienceSource.loop = true;
            ambienceSource.Play();
        }

        else
        {
            ambienceSource.Stop();
        }

        FadeIn();
    }

    public void FadeOut()
    {
        float endVolume = GetLinearToDecibel(audioConfig.MinVolume);
        audioConfig.MainMixer.DOSetFloat(MasterVolume, endVolume, 1f);
    }

    private void FadeIn()
    {
        audioConfig.MainMixer.DOSetFloat(MasterVolume, GetLinearToDecibel(audioConfig.MasterVolume), 1.5f);
    }

    private float GetLinearToDecibel(float volume)
    {
        float curvedValue = Mathf.Pow(volume, 2f);
        float safeLevel = Mathf.Max(curvedValue, audioConfig.MinVolume);

        return Mathf.Log10(safeLevel) * 20f;
    }

    private void PlayItemTransferredSFX() => PlaySFX(SFXType.ItemTransfer);
    private void PlayCountDownStartSFX() => PlaySFX(SFXType.Countdown);
    private void PlayTenSecBeepSFX(LevelTimerTickSignal signal)
    {
        if (signal.SecondsLeft > 10) return;

        PlaySFX(SFXType.Ten_Sec_Beep);
    }

    public void PlaySFX(SFXType type)
    {
        SFXEntry entry = audioConfig.SFXEntries.Find(x => x.SFXType == type);

        if (entry.SFX == null) return;

        sfxSource.pitch = Random.Range(entry.MinPitch, entry.MaxPitch);
        sfxSource.PlayOneShot(entry.SFX);
    }

    public void UpdateVolume(AudioType audioType, float value)
    {
        switch (audioType)
        {
            case AudioType.Master:
                audioConfig.MasterVolume = value;
                float masterVol = GetLinearToDecibel(value);
                audioConfig.MainMixer.SetFloat(MasterVolume, masterVol);
                break;

            case AudioType.Music:
                audioConfig.MusicVolume = value;
                float musicVol = GetLinearToDecibel(value);
                audioConfig.MainMixer.SetFloat(MusicVolume, musicVol);
                break;

            case AudioType.Ambience:
                audioConfig.AmbienceVolume = value;
                float ambienceVol = GetLinearToDecibel(value);
                audioConfig.MainMixer.SetFloat(AmbienceVolume, ambienceVol);
                break;

            case AudioType.SFX:
                audioConfig.SFXVolume = value;
                float sfxVol = GetLinearToDecibel(value);
                audioConfig.MainMixer.SetFloat(SFXVolume, sfxVol);
                audioConfig.MainMixer.SetFloat(UISFXVolume, sfxVol);
                break;

            default:
                break;
        }
    }

    public float GetVolume(AudioType audioType)
    {
        return audioType switch
        {
            AudioType.Master => audioConfig.MasterVolume,
            AudioType.Ambience => audioConfig.AmbienceVolume,
            AudioType.Music => audioConfig.MusicVolume,
            AudioType.SFX => audioConfig.SFXVolume,
            _ => audioConfig.MinVolume,
        };
    }
}