using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SfxConfigSO", menuName = "Scriptable Objects/New SfxConfigSO")]
public class SfxConfigSO : ScriptableObject
{
    public List<SfxEntry> sfxList = new();
}

[Serializable]
public struct SfxEntry
{
    public SfxType SfxType;
    public AudioClip Sfx;
}
