using System;
using System.Collections.Generic;
using UnityEngine;


public enum ProcessState { NotProcessed, Processing, Paused, Processed };

[RequireComponent(typeof(IngredientItem))]
[RequireComponent(typeof(ProgressDisplay))]
public class ItemBehaviourController : MonoBehaviour
{
    //References
    private IngredientItem ingredientItem;
    private ProgressDisplay progressDisplay;

    //Data-Lookup
    private Dictionary<(ProcessType, ItemStage), ProcessRule> ruleMap = new();

    //Process
    private ProgressTracker progressTracker = new();
    private ProcessRule currentProcess;
    [SerializeField] private ProcessState processState = ProcessState.NotProcessed;

    public ProgressTracker ProgressTracker { get => progressTracker; }

    //Events
    public event Action OnProcessComplete;

    private void Awake()
    {
        ingredientItem = GetComponent<IngredientItem>();
        progressDisplay = GetComponent<ProgressDisplay>();

        InitializeProcessRules();
    }

    #region Initialize Data Lookup

    private void InitializeProcessRules()
    {
        var data = ingredientItem.GetItemData;
        if (data.ProcessRules.Count <= 0) { return; }
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
        if (currentProcess == null) return;

        bool isBurning = currentProcess.ToStage == ItemStage.Burnt;

        if (processState != ProcessState.Paused)
            progressTracker.SetTarget(currentProcess.ProcessTime);


        progressDisplay.InitializeBar(currentProcess.ProcessTime, isBurning);
        processState = ProcessState.Processing;
    }

    public void HandleTickBehaviour(float deltaTime)
    {
        progressTracker.Tick(deltaTime);
        progressDisplay.UpdateBar(deltaTime);
        Debug.Log("progress ratio " + progressTracker.ProgressRatio);

        if (progressTracker.IsFinished)
        {
            HandleFinishBehaviour();
        }
    }

    private void HandleFinishBehaviour()
    {
        progressTracker.Reset();
        ingredientItem.SetItemStage(currentProcess.ToStage);
        ingredientItem.UpdateMesh(currentProcess.OutputMesh);
        currentProcess = null;
        ingredientItem.HandleItemUIState();
        progressDisplay.Hide();
        progressDisplay.ClearBar();
        OnProcessComplete?.Invoke();
    }

    public void HandlePauseProcess()
    {
        processState = ProcessState.Paused;

        if (ingredientItem.ItemStage == ItemStage.Cooked)
        {
            progressDisplay.Hide();
        }
    }
}