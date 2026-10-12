using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TutorialStepManager : MonoBehaviour
{
    //Zenject
    [Inject] private SignalBus signalBus;
    [Inject] private OrderManager orderManager;

    //Tutorial Targets
    private Dictionary<TutorialTargetType, List<TutorialCounterTarget>> targetPairs = new();

    [Header("Step Data")]
    [SerializeField] private TutorialStepSO tutorialSteps;
    [SerializeField] private int currentStepIndex = 0;

    [Header("Dialogue")]
    [SerializeField] private DialoguePanelDisplay dialoguePanel;

    [Header("Guidance")]
    [SerializeField] private GuidancePointer pointer;

    [Header("Modules")]
    [SerializeField] private List<ItemInteractionModule> interactionModules;
    [SerializeField] private List<IngredientItem> ingredients;
    [SerializeField] private List<Plate> plates;

    private void SubscribeToEvents()
    {
        signalBus.Subscribe<OrderDeliveredSignal>(OrderDelivered);

        var list = FindObjectsByType<TutorialCounterTarget>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (var target in list)
        {
            if (!targetPairs.TryGetValue(target.TargetType, out List<TutorialCounterTarget> targetList))
            {
                targetList = new();
                targetPairs[target.TargetType] = targetList;
            }

            targetPairs[target.TargetType].Add(target);
        }

        var interactionModules = FindObjectsByType<ItemInteractionModule>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (var interactionModule in interactionModules)
        {
            interactionModule.OnItemTransferred += ItemTransfferred;
            this.interactionModules.Add(interactionModule);
        }

        var ingredients = FindObjectsByType<IngredientItem>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (var ingredient in ingredients)
        {
            ingredient.OnItemProcessed += IngredientProcessed;
            this.ingredients.Add(ingredient);
        }

        var plates = FindObjectsByType<Plate>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (var plate in plates)
        {
            plate.OnItemAddedToPlate += ItemAddedToPlate;
            this.plates.Add(plate);
        }
    }

    private void UnsubscribeFromEvents()
    {
        signalBus.Unsubscribe<OrderDeliveredSignal>(OrderDelivered);

        foreach (var interaction in interactionModules)
        {
            interaction.OnItemTransferred -= ItemTransfferred;
        }
        interactionModules.Clear();

        foreach (var ingredient in ingredients)
        {
            ingredient.OnItemProcessed -= IngredientProcessed;
        }
        ingredients.Clear();

        foreach (var plate in plates)
        {
            plate.OnItemAddedToPlate -= ItemAddedToPlate;
        }
        plates.Clear();
    }

    //subs to gamephasechanged signal and start tutorial after preparation
    private void Start()
    {
        SubscribeToEvents();
        StartStep();
    }

    private void OnDisable()
    {
        UnsubscribeFromEvents();
    }

    private void StartStep()
    {
        TutorialStep step = tutorialSteps.Steps[currentStepIndex];

        foreach (var targetList in targetPairs.Values)
        {
            foreach (var target in targetList)
            {
                target.SetInteractionsEnabled(false);
                target.SetPointer(false);
            }
        }

        foreach (var allowedTarget in step.AllowedTargets)
        {
            if (targetPairs.TryGetValue(allowedTarget, out List<TutorialCounterTarget> targetList))
            {
                foreach (var target in targetList)
                {
                    target.SetInteractionsEnabled(true);
                }
            }
        }

        dialoguePanel.Show(step.Dialogue);

        if (step.ShowGuidance)
        {
            if (targetPairs.TryGetValue(step.GuidanceType, out List<TutorialCounterTarget> targetList))
            {
                foreach (var target in targetList)
                {
                    target.SetPointer(true, step.GuidanceIcon);
                }
            }
        }

        if (step.IsOrderSpawnRequired)
        {
            orderManager.SpawnOrder(step.OrderToSpawn);
        }
    }

    private void GetNextStep()
    {
        Debug.Log("Step Complete!");
        currentStepIndex++;
        StartStep();
    }

    private void ItemTransfferred(IInteractor from, IInteractor to, IPickable pickable)
    {
        TutorialStep step = tutorialSteps.Steps[currentStepIndex];

        if (step.CompletionCondition != TutorialCompletionCondition.GameplayEvent)
            return;
        if (step.GameplayEvent != GameplayEvent.ItemTransferred)
            return;
        if (step.RequiredItem != pickable.ItemType)
            return;

        Debug.Log($"{pickable.ItemType} is transferred from: {(from as Component).name} to: {(to as Component).name}.");

        GetNextStep();
    }

    private void IngredientProcessed(IngredientItem ingredientItem)
    {
        TutorialStep step = tutorialSteps.Steps[currentStepIndex];

        if (step.CompletionCondition != TutorialCompletionCondition.GameplayEvent)
            return;
        if (step.GameplayEvent != GameplayEvent.ItemProcessed)
            return;
        if (step.RequiredItem != ingredientItem.ItemType)
            return;
        if (step.PreparationStage != ingredientItem.PreparationStage)
            return;

        Debug.Log($"{ingredientItem.ItemType} is processed. Current preparation stage is: {ingredientItem.PreparationStage}.");

        GetNextStep();
    }

    private void ItemAddedToPlate(ContainerItem containerItem, IPickable pickable)
    {
        TutorialStep step = tutorialSteps.Steps[currentStepIndex];

        if (step.CompletionCondition != TutorialCompletionCondition.GameplayEvent)
            return;
        if (step.GameplayEvent != GameplayEvent.ItemAddedToContainer)
            return;
        if (step.RequiredItem != pickable.ItemType)
            return;

        Debug.Log($"{pickable.ItemType} is transferred to: {containerItem.name}.");

        GetNextStep();
    }

    private void OrderDelivered(OrderDeliveredSignal signal)
    {
        TutorialStep step = tutorialSteps.Steps[currentStepIndex];

        if (step.RequiredRecipe != signal.Order.Recipe)
            return;

        Debug.Log($"{signal.Order.Recipe} delivered succesfully!.");

        GetNextStep();
    }

    public void OnDialogueWindowSkipTap()
    {
        TutorialStep step = tutorialSteps.Steps[currentStepIndex];

        if (step.CompletionCondition != TutorialCompletionCondition.Tap)
            return;
        GetNextStep();
    }
}