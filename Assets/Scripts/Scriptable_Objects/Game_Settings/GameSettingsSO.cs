using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "New GameSettingsSO", menuName = "Scriptable Objects/New Game Settings SO")]
public class GameSettingsSO : ScriptableObject
{
    [Header("Audio Mixer")]
    public AudioMixer MainMixer;

    [Header("Snapshot Settings")]
    public float snapshotTransitionTime;
    public AudioMixerSnapshot normalSnapshot;
    public AudioMixerSnapshot muteSnapshot;
    public AudioMixerSnapshot pauseSnapshot;

    [Header("Audio Settings")]
    public float MinVolume = 0.00001f;
    public float fadeTime = 2f;
    public float MasterVolume = 0.5f;
    public float MusicVolume = 0.5f;
    public float AmbienceVolume = 0.5f;
    public float SFXVolume = 0.5f;
}
