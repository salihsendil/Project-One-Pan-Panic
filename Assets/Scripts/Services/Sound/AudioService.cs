using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AudioService : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    private Dictionary<SfxEntry, AudioClip> sfxDictionary = new();
    private void Awake()
    {

    }


    void Start()
    {
        musicSource.volume = 0f;

    }

    void Update()
    {

    }
}
