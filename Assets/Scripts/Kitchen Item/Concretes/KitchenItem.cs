using System.Collections.Generic;
using UnityEngine;

public class KitchenItem : MonoBehaviour
{
    private bool hasProcess;
    private IItemBehaviour currentBehaviour;

    private MeshFilter meshFilter;
    private ItemStage itemStage = ItemStage.Raw;
    private WorkStage workStage = WorkStage.Idle;
    private PlayerController playerController;
    [SerializeField] private KitchenItemSO kitchenItemSO;
    private Dictionary<ProcessType, IItemBehaviour> behavioursDict = new();
    private Dictionary<(ProcessType, ItemStage), ProcessRule> ruleMap = new();

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

    #region Handle Start Process
    public bool CanProcess(ProcessType type)
    {
        return ruleMap.ContainsKey((type, itemStage));
    }

    public bool TryStartProcess(ProcessType processType)
    {
        if (!hasProcess)
        {
            if (TryGetAppropriateProcess(processType, out currentBehaviour, out ProcessRule rule))
            {
                hasProcess = true;
                StartProcess(rule);
                return true;
            }
        }
        return false;
    }

    public bool TryGetAppropriateProcess(ProcessType processType, out IItemBehaviour behaviour, out ProcessRule rule)
    {
        behaviour = null;
        rule = null;

        if (!ruleMap.TryGetValue((processType, itemStage), out ProcessRule processRule)) { return false; }

        if (behavioursDict.TryGetValue(processRule.currentProcessType, out IItemBehaviour itemBehaviour))
        {
            behaviour = itemBehaviour;
            rule = processRule;
            return true;
        }
        return false;
    }

    #endregion

    #region Handle Process Pause

    public bool TryPauseProcess(ProcessType processType)
    {
        if (!behavioursDict.TryGetValue(processType, out IItemBehaviour itemBehaviour)) { return false; }

        if (currentBehaviour != itemBehaviour) { return false; }

        currentBehaviour.HandlePauseProcess(this);
        return true;
    }

    public void HandleProcessPauseState(bool isPaused, WorkStage behaviourWorkStage)
    {
        workStage = isPaused ? WorkStage.Idle : behaviourWorkStage;
    }

    #endregion

    private void StartProcess(ProcessRule rule)
    {
        currentBehaviour.OnProcessStarted += HandleProcessStarted;
        currentBehaviour.OnProcessComplete += ProcessComplete;
        currentBehaviour.StartProcess(this, rule);
    }

    private void HandleProcessStarted(WorkStage behaviourWorkStage)
    {
        workStage = behaviourWorkStage;
        currentBehaviour.OnProcessStarted -= HandleProcessStarted;
    }

    private void ProcessComplete(IItemBehaviour behaviour, ProcessRule rule)
    {
        hasProcess = false;
        currentBehaviour = null;
        behaviour.OnProcessComplete -= ProcessComplete;
        workStage = WorkStage.Idle;
        itemStage = rule.toStage;

        UpdateMesh(rule.outputMesh);

        //burasý uygun deðil ya 
        if (rule.currentProcessType == ProcessType.Cut)
        {
            UpdatePlayerBusyState(playerController);
        }

        TryStartProcess(rule.nextProcessType);
    }

    //burasý uygun deðil ya 
    public void UpdatePlayerBusyState(PlayerController player)
    {
        playerController = player;
        playerController.SetBusyState();
    }
}
