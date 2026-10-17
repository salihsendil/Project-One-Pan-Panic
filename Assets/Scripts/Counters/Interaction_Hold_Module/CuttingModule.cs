using UnityEngine;


// This module handles the cutting process.
// It has a process time variable and works with hold input.
// It requires an ItemSocket that implements IInteractor to set references and variables
// depending on which item is placed on the counter.
// When the item processing starts, the item cannot be picked up until the process is finished.
// UI (process bar display) and counter animation methods are called from this class.


[RequireComponent(typeof(CuttingCounter))]
[RequireComponent(typeof(CuttingCounterAnimationsController))]
public class CuttingModule : MonoBehaviour, IHoldInteractionModule
{
    [Header("References")]
    private IInteractor interactor;
    private CuttingCounterAnimationsController animationsController;

    [Header("Process")]
    [SerializeField] private bool isProcessing = false;
    [SerializeField] private ProcessType processType = ProcessType.Cut;
    [SerializeField] private float processTime = 3f;
    [SerializeField] private ProgressTracker progressTracker;

    [Header("Process Display")]
    [SerializeField] private ProcessDisplay processDisplay;

    [Header("Item")]
    [SerializeField] private bool hasItem = false;
    [SerializeField] private bool canProcessable = false;
    [SerializeField] private IngredientItem ingredientItem;
    [SerializeField] private ProcessSO process;


    private void Awake()
    {
        progressTracker = new();
        processDisplay = GetComponent<ProcessDisplay>();
        interactor = GetComponent<IInteractor>();
        animationsController = GetComponent<CuttingCounterAnimationsController>();
    }

    private void OnEnable()
    {
        interactor.OnItemPlaced += ItemPlaced;
        interactor.OnItemRemoved += ItemRemoved;
    }

    private void OnDisable()
    {
        interactor.OnItemPlaced -= ItemPlaced;
        interactor.OnItemRemoved -= ItemRemoved;
    }

    private void ItemPlaced(IPickable pickable)
    {
        hasItem = true;

        if (!pickable.Transform.TryGetComponent(out IngredientItem ingredientItem)) return;
        if (!ingredientItem.Transform.TryGetComponent(out IngredientProcessModule module)) return;
        if (!module.TryGetProcessSO(processType, out ProcessSO process)) return;

        this.ingredientItem = ingredientItem;
        this.process = process;

        canProcessable = true;

        progressTracker.Set(processTime);
    }

    private void ItemRemoved(IPickable pickable)
    {
        hasItem = false;
        canProcessable = false;

        ingredientItem = null;
        process = null;

        progressTracker.Reset();
    }


    public void OnHoldInteractionStarted()
    {
        if (canProcessable)
        {
            isProcessing = true;
            ingredientItem.IsPickable = false;
            processDisplay.Initialize();
            animationsController.UpdateAnimationState(true);
        }
    }


    public void OnHoldInteractionCanceled()
    {
        isProcessing = false;
        animationsController.UpdateAnimationState(false);
    }

    private void Update()
    {
        if (!isProcessing) return;
        if (!hasItem) return;
        if (!canProcessable) return;

        progressTracker.Tick(Time.deltaTime);
        processDisplay.Tick(progressTracker.ProgressRatio);

        if (progressTracker.IsFinished)
        {
            ingredientItem.PreparationStage = process.ToStage;

            if (process.VisualChangeTiming == VisualChangeTiming.OnProcessComplete)
                ingredientItem.UpdateVisual(process.OutputMesh, process.OutputMaterial);
            
            canProcessable = false;
            ingredientItem.IsPickable = true;

            animationsController.UpdateAnimationState(false);

            processDisplay.Hide();
            progressTracker.Reset();
        }

    }
}