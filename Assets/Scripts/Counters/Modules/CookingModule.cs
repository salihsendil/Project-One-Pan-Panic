using UnityEngine;

[RequireComponent(typeof(LoopSFXEmitter))]
[RequireComponent(typeof(ItemSocket))]
public class CookingModule : MonoBehaviour, IAutoModule
{
    //References
    private IInteractor interactor;

    //Process
    private ProcessType processType = ProcessType.PanCooked;
    private float processSpeed = 1f;
    private bool isProcessing;

    //Current Item
    private IItemProcess itemProcess;

    //SFX
    [SerializeField] private LoopSFXEmitter loopSfxEmitter;

    private void Awake()
    {
        interactor = GetComponent<IInteractor>();
        loopSfxEmitter = GetComponent<LoopSFXEmitter>();
    }

    public bool CanProcessable(IPickable pickable)
    {
        if (!pickable.GetGameObject.TryGetComponent(out ItemProcessHandler processHandler)) return false;
        if (!processHandler.TryGetProcess(processType, out itemProcess)) return false;

        return true;
    }

    public void StartProcess()
    {
        if (itemProcess == null) return;

        itemProcess.StartProcess();
        itemProcess.OnProcessFinished += CompleteProcess;

        loopSfxEmitter.PlaySFX();

        isProcessing = true;
    }

    public void PauseProcess()
    {
        isProcessing = false;

        if (itemProcess == null) return;

        loopSfxEmitter.StopSFX();

        itemProcess.PauseProcess();
        itemProcess.OnProcessFinished -= CompleteProcess;
        itemProcess = null;
    }

    public void CompleteProcess(IItemProcess process)
    {
        isProcessing = false;

        itemProcess.OnProcessFinished -= CompleteProcess;

        if (CanProcessable(interactor.GetItem))
        {
            StartProcess();
            return;
        }

        loopSfxEmitter.StopSFX();
    }

    private void Update()
    {
        if (isProcessing && itemProcess != null)
        {
            itemProcess.TickProcess(Time.deltaTime * processSpeed);
        }
    }
}