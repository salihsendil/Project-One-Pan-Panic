using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IngredientItem))]
public class ItemBehaviourController : MonoBehaviour
{
    //References
    private IngredientItem ingredientItem;

    //Stages
    private WorkStage workStage = WorkStage.Idle;

    //Data-Lookup
    private Dictionary<ProcessType, IItemBehaviour> behavioursDict = new();
    private Dictionary<(ProcessType, ItemStage), ProcessRule> ruleMap = new();

    //Getter
    public WorkStage WorkStage => workStage;

    //Events
    public event Action<ItemBehaviourController> OnItemBehaviourProcessComplete;

    private void Awake()
    {
        TryGetComponent(out ingredientItem);

        InitializeProcessRules();
        InitializeBehaviours();
    }
    private void InitializeProcessRules()
    {
        var data = ingredientItem.GetKitchenItemSO() as IngredientItemSO;
        if (data.ProcessRules.Count <= 0) { return; }
        foreach (var rule in data.ProcessRules)
        {
            ruleMap.TryAdd((rule.ProcessType, rule.FromStage), rule);
        }
    }

    private void InitializeBehaviours()
    {
        var list = GetComponents<IItemBehaviour>();
        foreach (var item in list)
        {
            behavioursDict.TryAdd(item.GetProcessType(), item);
        }
    }

    public bool CanProcess(ProcessType type)
    {
        return ruleMap.ContainsKey((type, ingredientItem.ItemStage)) && behavioursDict.ContainsKey(type);
    }

    public void HandleProcessStart(ProcessType processType)
    {
        if (!ruleMap.TryGetValue((processType, ingredientItem.ItemStage), out ProcessRule rule)) { return; }

        if (!behavioursDict.TryGetValue(processType, out IItemBehaviour behaviour)) { return; }

        workStage = WorkStage.Processing;

        behaviour.OnProcessComplete += HandleProcessComplete;

        behaviour.StartProcess(rule);
    }

    private void HandleProcessComplete(IItemBehaviour behaviour, ProcessRule rule)
    {
        behaviour.OnProcessComplete -= HandleProcessComplete;

        ingredientItem.SetItemStage(rule.ToStage);

        workStage = WorkStage.Idle;

        ingredientItem.UpdateMesh(rule.OutputMesh);

        OnItemBehaviourProcessComplete?.Invoke(this);
    }

    public void HandlePauseProcess(ProcessType processType)
    {
        if (!TryGetBehaviour(processType, out IItemBehaviour behaviour)) { return; }

        behaviour.OnProcessComplete -= HandleProcessComplete;

        behaviour.SetProcessPause(true);

        workStage = WorkStage.Paused;
    }

    public void HandleResumeProcess(ProcessType processType)
    {
        if (!TryGetBehaviour(processType, out IItemBehaviour behaviour)) { return; }

        behaviour.OnProcessComplete += HandleProcessComplete;

        behaviour.SetProcessPause(false);

        workStage = WorkStage.Processing;
    }

    private bool TryGetBehaviour(ProcessType processType, out IItemBehaviour behaviour)
    {
        behaviour = null;

        if (!CanProcess(processType)) { return false; }

        if (!behavioursDict.TryGetValue(processType, out behaviour)) { return false; }

        return true;
    }
}
