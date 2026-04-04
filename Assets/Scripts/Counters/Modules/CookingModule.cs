using UnityEngine;

[RequireComponent(typeof(ItemSocket))]
public class CookingModule : MonoBehaviour, IAutoModule
{
    private bool isProcessing;
    private float processSpeed = 1f;
    private ProcessType processType = ProcessType.PanCooked;
    private ItemBehaviourController currentBehaviour;
    private IInteractor interactor;

    private void Awake()
    {
        interactor = GetComponent<IInteractor>();
    }

    public bool CanProcessable(IPickable pickable)
    {
        if (!pickable.GetGameObject.TryGetComponent(out ItemBehaviourController controller)) return false;
        if (!controller.CanProcess(processType)) return false;
        return true;
    }

    public void StartProcess(IPickable pickable)
    {
        if (!pickable.GetGameObject.TryGetComponent(out currentBehaviour)) return;
        isProcessing = true;
        currentBehaviour.HandleStartBehaviour();
        currentBehaviour.OnProcessComplete += CompleteProcess;
    }

    public void StopProcess()
    {
        isProcessing = false;
        if (currentBehaviour == null) return;

        currentBehaviour.OnProcessComplete -= CompleteProcess;
        currentBehaviour = null;
    }

    public void CompleteProcess()
    {
        isProcessing = false;
        if (currentBehaviour == null) return;

        currentBehaviour.OnProcessComplete -= CompleteProcess;
        currentBehaviour = null;

        if (CanProcessable(interactor.GetItem))
        {
            StartProcess(interactor.GetItem);
        }
    }

    private void LateUpdate()
    {
        if (isProcessing && interactor.HasItem && currentBehaviour != null)
        {
            currentBehaviour.HandleTickBehaviour(Time.deltaTime * processSpeed);
        }
    }


}