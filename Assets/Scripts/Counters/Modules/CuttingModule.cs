using UnityEngine;
using Zenject;

[RequireComponent(typeof(ItemSocket))]
[RequireComponent(typeof(CuttingCounterAnimationsController))]
public class CuttingModule : MonoBehaviour, IHoldModule
{
    //Zenject
    [Inject] private SFXService sfxService;

    //References
    private IInteractor interactor;
    private CuttingCounterAnimationsController animationsController;

    //Process
    private ProcessType processType = ProcessType.Cut;
    private float processSpeed = 1f;
    private bool isProcessing;

    //Current Item
    private IItemProcess itemProcess;

    private void Awake()
    {
        interactor = GetComponent<IInteractor>();
        animationsController = GetComponent<CuttingCounterAnimationsController>();
    }

    public void OnInteractionStarted()
    {
        if (!interactor.HasItem) return;
        if (!interactor.GetItem.GetGameObject.TryGetComponent(out ItemProcessHandler processHandler)) return;
        if (processHandler.TryGetProcess(processType, out itemProcess)) return;
    }

    public void OnInteractionPerformed()
    {
        if (itemProcess == null) return;
        interactor.GetItem.IsPickable = false;

        itemProcess.StartProcess();
        itemProcess.OnProcessFinished += HandleProcessFinish;
        
        isProcessing = true;
        animationsController.UpdateAnimationState(isProcessing);
    }

    public void OnInteractionCanceled()
    {
        isProcessing = false;
        animationsController.UpdateAnimationState(isProcessing);

        if (itemProcess == null) return;

        itemProcess.PauseProcess();
        itemProcess.OnProcessFinished -= HandleProcessFinish;
        itemProcess = null;
    }

    private void HandleProcessFinish(IItemProcess _)
    {
        isProcessing = false;
        animationsController.UpdateAnimationState(isProcessing);

        itemProcess.OnProcessFinished -= HandleProcessFinish;
        itemProcess = null;

        interactor.GetItem.IsPickable = true;
    }

    private void PlaySFX()
    {
        sfxService.PlaySFXOneShot(SFXType.IngredientCut);
    }

    private void Update()
    {
        if (isProcessing && itemProcess != null)
        {
            itemProcess.TickProcess(Time.deltaTime * processSpeed);
        }
    }
}
