using UnityEngine;
using Zenject;

[RequireComponent(typeof(ItemProcessDisplay))]
public class CookableProcess : BaseItemProcess
{
    [Inject] private AudioService audioService;
    [SerializeField] private SFXType sfxType;
    public override void StartProcess()
    {
        base.StartProcess();
        audioService.PlaySFX(sfxType);
    }

    public override void PauseProcess()
    {
        base.PauseProcess();
    }

    public override void FinishProcess()
    {
        base.FinishProcess();
    }





    private void Awake()
    {
        processDisplayer = GetComponent<ItemProcessDisplay>();
    }
}
