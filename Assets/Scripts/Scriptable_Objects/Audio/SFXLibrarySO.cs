using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SFX Library SO", menuName = "Scriptable Objects/New SFX Library SO")]
public class SFXLibrarySO : ScriptableObject
{
    public List<SFXEntry> entries = new();
}

[Serializable]
public struct SFXEntry
{
    public SFXType SFXType;
    public AudioClip SFX;
    public float MinPitch;
    public float MaxPitch;
}