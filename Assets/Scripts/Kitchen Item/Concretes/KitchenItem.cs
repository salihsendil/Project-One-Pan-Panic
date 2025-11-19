using System.Collections.Generic;
using UnityEngine;

public class KitchenItem : MonoBehaviour
{
    private MeshFilter meshFilter;
    private int currentStageIndex = 0;
    [SerializeField] private KitchenItemSO kitchenItemSO;
    private Dictionary<ProcessType, IItemBehaviour> behavioursDict = new();
    private Dictionary<(ProcessType, int), ProcessRule> ruleMap = new();

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

    public void StartProcess(ProcessType processType)
    {
        ruleMap.TryGetValue((processType, currentStageIndex), out ProcessRule rule);
        if (rule == null) { return; }

        IItemBehaviour behaviour = behavioursDict[rule.processType];
        behaviour.StartProcess(this, rule);
    }

    public bool CanProcess(ProcessType type)
    {
        return ruleMap.ContainsKey((type, currentStageIndex));
    }
}
