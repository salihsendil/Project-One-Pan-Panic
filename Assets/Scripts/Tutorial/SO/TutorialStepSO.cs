using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New_TutorialStepSO", menuName = "Scriptable Objects/New TutorialStepSO")]
public class TutorialStepSO : ScriptableObject
{
    public List<TutorialStep> Steps = new();
}

[Serializable]
public class TutorialStep
{
    [Header("UI")]
    [TextArea]
    public string Dialogue;

    [Header("Conditions")]
    public TutorialCompletionCondition CompletionCondition;
    public GameplayEvent GameplayEvent;
    public List<TutorialTargetType> AllowedTargets;
    public ItemType RequiredItem;
    public PreparationStage PreparationStage;
    public RecipeSO RequiredRecipe;

    [Header("Guadiance")]
    public bool ShowGuidance;
    public TutorialTargetType GuidanceType; // ayný anda iki farklý hedef göstermek için biþeyler yapabilir miyiz?
    public Sprite GuidanceIcon;

    [Header("Extra")]
    public bool IsOrderSpawnRequired;
    public RecipeSO OrderToSpawn;
}

public enum TutorialCompletionCondition
{
    None,
    Tap = 5,
    GameplayEvent
}

public enum GameplayEvent
{
    None,
    ItemTransferred,
    ItemProcessed,
    ItemAddedToContainer,
    OrderDelivered
}

public enum TutorialTargetType
{
    None,
    HolderCounter,
    CuttingCounter,
    TomatoDispenser,
    LettuceDispenser,
    PlateDispenser,
    DeliveryCounter
}