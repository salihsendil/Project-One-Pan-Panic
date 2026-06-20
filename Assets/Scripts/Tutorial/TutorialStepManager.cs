using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TutorialStepManager : MonoBehaviour
{
    [Inject] private SignalBus signalBus;
    [Inject] private OrderManager orderManager;
    [Inject] private GameManager gameManager;

    [SerializeField] private TutorialStepSO tutorialStepData;
    [SerializeField] private int currentStepIndex = 0;

    [SerializeField] private ObjectivePointer pointer;
    [SerializeField] private List<TutorialTarget> pointerTargets;

    private void Awake()
    {
        if (pointer == null) FindObjectsByType<ObjectivePointer>(FindObjectsSortMode.None);
    }

    private void OnEnable()
    {
        signalBus.Subscribe<ItemTransferredSignal>(OnItemTransferred);
        signalBus.Subscribe<ItemProcessedSignal>(OnItemProcessed);
        signalBus.Subscribe<IngredientAddedToContainerSignal>(OnIngredientAddedToContainer);
        signalBus.Subscribe<OrderDeliveredSignal>(OnOrderDelivered);
    }

    private void OnDisable()
    {
        signalBus.Unsubscribe<ItemTransferredSignal>(OnItemTransferred);
        signalBus.Unsubscribe<ItemProcessedSignal>(OnItemProcessed);
        signalBus.Unsubscribe<IngredientAddedToContainerSignal>(OnIngredientAddedToContainer);
        signalBus.Unsubscribe<OrderDeliveredSignal>(OnOrderDelivered);
    }

    private void Start()
    {
        GetCurrentStep();
    }

    private void OnItemTransferred(ItemTransferredSignal signal)
    {
        if (TryCompleteCurrentStep(signal.GameplayEventType, signal.ItemType))
        {
            if (signal.To.InteractorType != tutorialStepData.Steps[currentStepIndex].TargetInteractor) return;
            currentStepIndex++;
            GetCurrentStep();
        }
    }

    private void OnItemProcessed(ItemProcessedSignal signal)
    {
        if (TryCompleteCurrentStep(signal.GameplayEventType, signal.ItemType))
        {
            currentStepIndex++;
            GetCurrentStep();
        }
    }

    private void OnIngredientAddedToContainer(IngredientAddedToContainerSignal signal)
    {
        if (TryCompleteCurrentStep(signal.GameplayEvent, signal.ItemType))
        {
            currentStepIndex++;
            GetCurrentStep();
        }
    }

    private void OnOrderDelivered(OrderDeliveredSignal signal)
    {
        TutorialStep step = tutorialStepData.Steps[currentStepIndex];

        if (step.CompletionCondition == TutorialCompletionCondition.GameplayEvent && signal.Order.Recipe == step.RequiredRecipe)
        {
            currentStepIndex++;
            GetCurrentStep();
        }
    }

    private bool TryCompleteCurrentStep(GameplayEvent eventType, ItemType targetType)
    {
        TutorialStep step = tutorialStepData.Steps[currentStepIndex];
        return step.CompletionCondition == TutorialCompletionCondition.GameplayEvent
               && eventType == step.GameplayEvent
               && targetType == step.RequiredItemType;
    }

    private void CheckStepAction(TutorialStep step)
    {
        TutorialStepAction stepAction = step.TutorialStepAction;

        switch (stepAction)
        {
            case TutorialStepAction.None:
                break;
            case TutorialStepAction.SpawnOrder:
                orderManager.GenerateOrder(step.TargetRecipe);
                break;
            default:
                break;
        }
    }

    public void OnScreenTap()
    {
        TutorialStep step = tutorialStepData.Steps[currentStepIndex];
        if (step.CompletionCondition == TutorialCompletionCondition.Tap)
        {
            currentStepIndex++;
            GetCurrentStep();
        }
    }

    public void GetCurrentStep()
    {
        if (currentStepIndex >= tutorialStepData.Steps.Count)
        {
            gameManager.SetPlayPhase();
            enabled = false;
            return;
        }

        TutorialStep tutorialStep = tutorialStepData.Steps[currentStepIndex];

        if (tutorialStep.WorldTarget == TutorialWorldTarget.None) pointer.HidePointer();

        else
        {
            foreach (var target in pointerTargets)
            {
                if (target.TutorialWorldTarget == tutorialStep.WorldTarget)
                {
                    pointer.SetPointer(target.transform.position, tutorialStep.GuidanceIcon);
                }
            }
        }

        CheckStepAction(tutorialStep);
        signalBus.Fire(new NewTutorialStepSignal(tutorialStep));
    }
}
