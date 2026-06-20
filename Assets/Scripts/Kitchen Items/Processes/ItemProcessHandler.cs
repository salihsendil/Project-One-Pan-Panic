using UnityEngine;
using Zenject;

[RequireComponent(typeof(IngredientItem))]
public class ItemProcessHandler : MonoBehaviour
{
    //Zenject
    [Inject] private SignalBus signalBus;

    private IngredientItem ingredient;
    private IItemProcess[] processes = new IItemProcess[3];

    private void Awake()
    {
        ingredient = GetComponent<IngredientItem>();
        processes = GetComponents<IItemProcess>();
    }

    private void OnEnable()
    {
        //subs  process events
        foreach (var process in processes)
        {
            process.OnProcessFinished += HandleProcessFinish;
        }
    }

    private void OnDisable()
    {
        //unsubs process events
        foreach (var process in processes)
        {
            process.OnProcessFinished -= HandleProcessFinish;
        }
    }

    public bool TryGetProcess(ProcessType processType, out IItemProcess itemProcess)
    {
        itemProcess = null;

        foreach (var process in processes)
        {
            if (process == null) continue;
            if (process.CanProcess(processType, ingredient.ItemStage))
            {
                itemProcess = process;
                return true;
            }
        }
        return false;
    }

    private void HandleProcessFinish(IItemProcess process)
    {
        ProcessRuleSO processRule = process.ProcessRule;

        ingredient.ItemProcessed(processRule.OutputMesh, processRule.ToStage);

        signalBus.Fire(new ItemProcessedSignal(GameplayEvent.IngredientProcessed,
                                                ingredient.IngredientData.ItemType,
                                                processRule.ProcessType));
    }
}
