using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "New Audio Config SO", menuName = "Scriptable Objects/Config/New Audio Config SO")]
public class AudioConfigSO : ScriptableObject
{
    [Header("General")]
    public AudioMixer MainMixer;

    [Header("Volumes")]
    public float MinVolume = 0.00001f;
    public float MasterVolume = 0.5f;
    public float MusicVolume = 0.5f;
    public float AmbienceVolume = 0.5f;
    public float SFXVolume = 0.5f;

    [Header("Profiles")]
    public List<AudioProfile> Profiles = new();

    [Header("SFX")]
    public List<SFXEntry> SFXEntries = new();
}

[Serializable]
public class AudioProfile
{
    public ScenesEnum Scene;
    public AudioClip Music;
    public AudioClip Ambience;
}

[Serializable]
public struct SFXEntry
{
    public SFXType SFXType;
    public AudioClip SFX;
    public float MinPitch;
    public float MaxPitch;
}