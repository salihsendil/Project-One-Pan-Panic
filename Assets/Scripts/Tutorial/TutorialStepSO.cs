using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TutorialStepSO", menuName = "Scriptable Objects/New TutorialStepSO")]
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
    public Sprite Mascot;

    [Header("Condition")]
    public TutorialCompletionCondition CompletionCondition;
    public GameplayEvent GameplayEvent;
    public InteractorType TargetInteractor;
    public ItemType RequiredItemType;
    public ProcessType ProcessType;
    public RecipeSO RequiredRecipe;

    [Header("Guidance")]
    public TutorialWorldTarget WorldTarget;
    public Sprite GuidanceIcon;

    [Header("Step Action")]
    public TutorialStepAction TutorialStepAction = TutorialStepAction.None;
    public RecipeSO TargetRecipe;
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
    ItemPickedUp = 5,
    IngredientProcessed,
    IngredientAddedToContainer,
    OrderDelivered
}

public enum TutorialWorldTarget
{
    None,
    TomatoCounter = 5,
    LettuceCounter,
    CuttingCounter_0,
    CuttingCounter_1,
    ContainerCounter,
    DeliveryCounter
}

public enum TutorialStepAction
{
    None,
    SpawnOrder = 5,
}

//[Flags]
//public enum TutorialType
//{
//    None = 0,
//    Dialogue = 1 << 0,
//    DisplayOnUI = 1 << 1,
//    DisplayOnMap = 1 << 2,
//}
