using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SFXService : MonoBehaviour
{
    [Inject] private SFXLibrarySO sfxLibrary;

    private int lastOneShotSourceIndex = 0;
    [SerializeField] private List<AudioSource> oneShotSources;
    [SerializeField] private AudioSource loopSource;

    private Dictionary<LoopSFXEmitter, AudioSource> loopSourcePool = new();

    public void RegisterLoopSFXEmitter(LoopSFXEmitter emitter)
    {
        if (loopSourcePool.TryGetValue(emitter, out AudioSource source)) return;

        source = Instantiate(loopSource, transform);
        loopSourcePool[emitter] = source;
    }
    public void UnregisterLoopSFXEmitter(LoopSFXEmitter emitter)
    {
        if (!loopSourcePool.TryGetValue(emitter, out AudioSource source)) return;
        Destroy(source.gameObject);
        loopSourcePool.Remove(emitter);
    }

    public void PlaySFXOneShot(SFXType type)
    {
        SFXEntry entry = sfxLibrary.entries.Find(x => x.SFXType == type);

        if (entry.SFX == null) return;

        lastOneShotSourceIndex++;
        lastOneShotSourceIndex %= oneShotSources.Count;

        float pitch = Random.Range(entry.MinPitch, entry.MaxPitch);
        oneShotSources[lastOneShotSourceIndex].pitch = pitch;

        oneShotSources[lastOneShotSourceIndex].PlayOneShot(entry.SFX);

    }

    public void PlayLoopSFX(LoopSFXEmitter emitter, SFXType type)
    {
        if (!loopSourcePool.TryGetValue(emitter, out AudioSource source))
        {
            source = Instantiate(loopSource, transform);
            loopSourcePool[emitter] = source;
        }

        if (source.isPlaying) return;

        SFXEntry entry = sfxLibrary.entries.Find(x => x.SFXType == type);

        if (entry.SFX == null) return;

        float pitch = Random.Range(entry.MinPitch, entry.MaxPitch);
        source.pitch = pitch;

        source.resource = entry.SFX;
        source.loop = true;
        source.Play();
    }

    public void StopLoopSFX(LoopSFXEmitter emitter)
    {
        if (!loopSourcePool.ContainsKey(emitter)) return;

        loopSourcePool[emitter].Stop();
    }

}
