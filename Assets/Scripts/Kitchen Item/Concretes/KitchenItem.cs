using System;
using System.Collections.Generic;
using UnityEngine;

public class KitchenItem : MonoBehaviour
{
    //References
    private MeshFilter meshFilter;

    //Data
    [SerializeField] private KitchenItemSO kitchenItemSO;

    //Stages
    [SerializeField] private ItemStage itemStage = ItemStage.Raw;
    [SerializeField] private WorkStage workStage = WorkStage.Idle;

    //Data-Lookup
    private Dictionary<ProcessType, IItemBehaviour> behavioursDict = new();
    private Dictionary<(ProcessType, ItemStage), ProcessRule> ruleMap = new();

    //Getter
    public WorkStage WorkStage => workStage;

    //Events
    public event Action<KitchenItem> OnItemProcessComplete;

    private void Awake()
    {
        TryGetComponent(out meshFilter);
        InitializeProcessRules();
        InitializeBehaviours();
    }
    private void InitializeProcessRules()
    {
        if (kitchenItemSO.processRules.Count <= 0) { return; }
        foreach (var rule in kitchenItemSO.processRules)
        {
            ruleMap.TryAdd((rule.currentProcessType, rule.fromStage), rule);
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

    public void UpdateMesh(Mesh newMesh)
    {
        if (meshFilter != null)
        {
            meshFilter.mesh = newMesh;
        }
    }

    public bool CanProcess(ProcessType type)
    {
        return ruleMap.ContainsKey((type, itemStage)) && behavioursDict.ContainsKey(type);
    }

    public void HandleProcessStart(ProcessType processType)
    {
        if (!ruleMap.TryGetValue((processType, itemStage), out ProcessRule rule)) { return; }

        if (!behavioursDict.TryGetValue(processType, out IItemBehaviour behaviour)) { return; }

        workStage = WorkStage.Processing;

        behaviour.OnProcessComplete += HandleProcessComplete;

        behaviour.StartProcess(this, rule);
    }

    private void HandleProcessComplete(IItemBehaviour behaviour, ProcessRule rule)
    {
        behaviour.OnProcessComplete -= HandleProcessComplete;

        itemStage = rule.toStage;

        workStage = WorkStage.Idle;

        UpdateMesh(rule.outputMesh);

        OnItemProcessComplete?.Invoke(this);
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
