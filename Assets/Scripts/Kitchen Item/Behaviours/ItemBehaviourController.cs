using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IngredientItem))]
public class ItemBehaviourController : MonoBehaviour
{
    //References
    private IngredientItem ingredientItem;

    //Data-Lookup
    private Dictionary<(ProcessType, ItemStage), ProcessRule> ruleMap = new();

    //Process
    private ProgressTracker progressTracker = new();
    private ProcessRule currentProcess;

    public ProgressTracker ProgressTracker { get => progressTracker; }

    //Events
    public event Action OnProcessComplete;

    private void Awake()
    {
        ingredientItem = GetComponent<IngredientItem>();

        InitializeProcessRules();
    }

    #region Initialize Data Lookup

    private void InitializeProcessRules()
    {
        var data = ingredientItem.GetItemData;
        if ( data.ProcessRules.Count <= 0) { return; }
        foreach (var rule in data.ProcessRules)
        {
            ruleMap.TryAdd((rule.ProcessType, rule.FromStage), rule);
        }
    }

    #endregion

    public bool CanProcess(ProcessType processType)
    {
        if (ruleMap.TryGetValue((processType, ingredientItem.ItemStage), out currentProcess))
        {
            return true;
        }

        return false;
    }

    public void HandleStartBehaviour()
    {
        Debug.Log("behaviour start");
        if (!progressTracker.IsFinished) return;

        if (currentProcess == null) return;

        progressTracker.SetTarget(currentProcess.ProcessTime);
    }

    public void HandleTickBehaviour(float deltaTime)
    {
        progressTracker.Tick(deltaTime);

        Debug.Log("progress ratio " + progressTracker.ProgressRatio);

        if (progressTracker.IsFinished)
        {
            HandleFinishBehaviour();
        }
    }

    private void HandleFinishBehaviour()
    {
        Debug.Log("behaviour finish");
        progressTracker.Reset();
        ingredientItem.SetItemStage(currentProcess.ToStage);
        ingredientItem.UpdateMesh(currentProcess.OutputMesh);
        currentProcess = null;
        ingredientItem.HandleItemUIState();
        OnProcessComplete?.Invoke();
    }

}