using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(ItemSocket))]
public class CuttingModule : MonoBehaviour, IHoldModule
{
    //References
    private ItemSocket itemSocket;
    private ItemBehaviourController behaviourController;

    //Process
    private ProcessType processType = ProcessType.Cut;
    private float processSpeed = 1f;
    private bool isProcessing;
    private bool canProcess;

    private void Awake()
    {
        itemSocket = GetComponent<ItemSocket>();
    }

    public void OnInteractionStarted()
    {
        if (!itemSocket.HasItem) return;
        if (!itemSocket.GetItem.GetGameObject.TryGetComponent(out behaviourController)) return;
        if (!behaviourController.CanProcess(processType)) return;
        canProcess = true;
    }

    public void OnInteractionPerformed()
    {
        if (!canProcess || isProcessing) return;

        itemSocket.GetItem.IsPickable = false;

        isProcessing = true;

        behaviourController.HandleStartBehaviour();

        behaviourController.OnProcessComplete += ProcessFinished;
    }

    public void OnInteractionCanceled()
    {
        isProcessing = false;
        canProcess = false;

        if (itemSocket.HasItem) { itemSocket.GetItem.IsPickable = true; }

        if (behaviourController != null)
        {
            behaviourController.OnProcessComplete -= ProcessFinished;
            behaviourController = null;
        }
    }

    private void ProcessFinished()
    {
        isProcessing = false;
        canProcess = false;

        if (itemSocket.HasItem) { itemSocket.GetItem.IsPickable = true; }

        if (behaviourController != null)
        {
            behaviourController.OnProcessComplete -= ProcessFinished;
            behaviourController = null;
        }
    }

    private void Update()
    {
        if (isProcessing)
        {
            if (behaviourController != null && canProcess)
            {
                behaviourController.HandleTickBehaviour(processSpeed * Time.deltaTime);
            }
        }
    }
}
