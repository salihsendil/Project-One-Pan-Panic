using UnityEngine;
using Zenject;

public class LoopSFXEmitter : MonoBehaviour
{
    [Inject] private SFXService sfxService;

    [SerializeField] private SFXType sfxType;

    private void OnEnable()
    {
        sfxService.RegisterLoopSFXEmitter(this);
    }

    private void OnDestroy()
    {
        sfxService.UnregisterLoopSFXEmitter(this);
    }

    public void PlaySFX()
    {
        sfxService.PlayLoopSFX(this, sfxType);
    }

    public void StopSFX()
    {
        sfxService.StopLoopSFX(this);
    }
}
