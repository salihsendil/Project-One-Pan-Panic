using System.Collections.Generic;
using UnityEngine;

public class KitchenItem : MonoBehaviour
{
    private MeshFilter meshFilter;
    [SerializeField] private ItemStage itemStage = ItemStage.Raw;
    [SerializeField] private WorkStage workStage = WorkStage.Idle;
    [SerializeField] private KitchenItemSO kitchenItemSO;
    private Dictionary<ProcessType, IItemBehaviour> behavioursDict = new();
    private Dictionary<(ProcessType, ItemStage), ProcessRule> ruleMap = new();


    public ItemStage ItemStage { get => itemStage; }
    public WorkStage WorkStage { get => workStage; set => workStage = value; }

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
            ruleMap.TryAdd((rule.processType, rule.fromStage), rule);
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
        return ruleMap.ContainsKey((type, itemStage));
    }

    public void StartProcess(ProcessType processType)
    {
        ruleMap.TryGetValue((processType, itemStage), out ProcessRule rule);
        if (rule == null) { return; }

        behavioursDict.TryGetValue(rule.processType, out IItemBehaviour behaviour);
        if (behaviour == null) { return; }

        behaviour.OnProcessComplete -= HandleProcessComplete;
        behaviour.OnProcessComplete += HandleProcessComplete;
        behaviour.HandleProcess(this, rule);
    }

    private void HandleProcessComplete(IItemBehaviour behaviour, ProcessRule rule)
    {
        behaviour.OnProcessComplete -= HandleProcessComplete;
        workStage = WorkStage.Idle;
        itemStage = rule.toStage;
        UpdateMesh(rule.outputMesh);
        StartProcess(rule.processType); //????
    }

}
