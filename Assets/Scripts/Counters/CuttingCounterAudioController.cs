using UnityEngine;
using Zenject;

public class CuttingCounterAudioController : MonoBehaviour
{
    [Inject] private AudioService audioService;
    [SerializeField] private SFXType sfxType;

    public void PlaySFX()
    {
        if (sfxType == SFXType.None) return;
        audioService.PlaySFX(sfxType);
    }
}
