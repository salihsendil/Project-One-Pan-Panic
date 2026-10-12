using System.Collections.Generic;
using UnityEngine;


// This class stores the available processes for the related ingredient.
// Counters or processor items can call this class to get a suitable process.
//
// This class has ingredient item references to access their preparation stage.


[RequireComponent(typeof(IngredientItem))]
public class IngredientProcessModule : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private IngredientItem ingredient;
    [SerializeField] private List<ProcessSO> processes = new();


    private void Start()
    {
        ingredient = GetComponent<IngredientItem>();
    }

    public bool TryGetProcessSO(ProcessType processType, out ProcessSO processSO)
    {
        processSO = null;

        foreach (var process in processes)
        {
            if (process.ProcessType == processType && ingredient.PreparationStage == process.FromStage)
            {
                processSO = process;
                return true;
            }
        }

        return false;
    }
}
