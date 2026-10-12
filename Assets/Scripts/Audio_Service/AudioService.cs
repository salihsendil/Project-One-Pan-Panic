using DG.Tweening;
using UnityEngine;
using Zenject;

public class AudioService : MonoBehaviour
{
    //Const
    private const string MasterVolume = "Master_Vol";

    //Zenject
    [Inject] private GameSettingsSO settingsSO;

    //Data
    [SerializeField] private AudioProfileSO audioProfile;

    //References
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource ambienceSource;

    private void PlayMusic()
    {
        if (musicSource != null && audioProfile.Music != null)
        {
            musicSource.resource = audioProfile.Music;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    private void PlayAmbience()
    {
        if (audioProfile.Ambience == null)
        {
            ambienceSource.Stop();
            return;
        }

        if (ambienceSource != null)
        {
            ambienceSource.resource = audioProfile.Ambience;
            ambienceSource.loop = true;
            ambienceSource.Play();
        }
    }

    public void SetProfileData(AudioProfileSO profileSO)
    {
        audioProfile = profileSO;

        PlayMusic();
        PlayAmbience();
        FadeIn();
    }

    private void FadeIn()
    {
        float endVolume = LinearToDecibel(settingsSO.MasterVolume);
        settingsSO.MainMixer.DOSetFloat(MasterVolume, endVolume, settingsSO.fadeTime);
    }

    public void FadeOut()
    {
        float endVolume = LinearToDecibel(settingsSO.MinVolume);
        settingsSO.MainMixer.DOSetFloat(MasterVolume, endVolume, settingsSO.fadeTime);
    }

    private float LinearToDecibel(float volume)
    {
        float curvedValue = Mathf.Pow(volume, 2f);
        float safeLevel = Mathf.Max(curvedValue, settingsSO.MinVolume);

        return Mathf.Log10(safeLevel) * 20f;
    }
}
