using System.Collections.Generic;
using UnityEngine;

// Main class for all cookwares (pan, pot, etc.).
// It provides cookware data, current state (cookware state), progress tracker, and process display.
//
// It has receive, import, and export item methods, but import is not working right now.
// So, it cannot import items from any container.
//
// When an ingredient is added, the cookware checks if the ingredient has a process module
// and if that module has a suitable process. If anything returns false, the cookware does not accept the item.
// The cookware process type is stored in the cookware data.
//
// When an item is added, a new ContainerSlot is created.
// This class holds some variables related to the item.
// Every item needs its own slot.
//
// Straight after an item is added, the cookware state is re-evaluated,
// because the item will not have been processed yet.
//
// The processing logic is as follows:
// Items are individual, but the process is shared.
// For example, if there are 2 items in the cookware and they are currently in the burning process,
// adding a new item changes the state back to processing.
// The progress tracker's current value is set to the completed progress time for 2 items,
// and the target is set to the process time required for 3 items.
// This means that each item is not processed individually.
// The process is shared between all items in the cookware.
//
// When an item is added to the cookware, the cookware must externally hide the item's icon display
// and show or update its own icon display.
//
// When processing is finished, each slot's item preparation state is updated according to the ProcessSO,
// and the update mesh method is called.
//
// IMPORTANT NOTE
// Processing depends on some parameters.
// The cookware must be on a suitable counter so it can become hot (isHot variable).
// The cookware cannot be empty or burnt.
// When the process finishes, the cookware state will be updated.
//
// Cookware state means the state that the items will have when the process finishes.
// For example, if the cookware state is Burning, the items will be burnt when the process finishes.
// If the state is Processing, the items will be processed when the process finishes.


public class CookwareItem : ContainerItem
{
    [Header("Config")]
    [SerializeField] private CookwareItemSO cookwareItemSO;

    [Header("State")]
    [SerializeField] private bool isHot;
    [SerializeField] private CookwareState cookwareState;

    [Header("Process")]
    [SerializeField] private ProgressTracker progressTracker;
    private IProcessView currentProcessView;
    [SerializeField] private ProcessDisplay processDisplay;
    [SerializeField] private ProcessDangerAlert dangerAlert;


    public bool IsHot { set => isHot = value; }

    protected override void Awake()
    {
        base.Awake();

        if (containerItemSO != null)
        {
            cookwareItemSO = (CookwareItemSO)containerItemSO;
        }

        cookwareState = CookwareState.Empty;
        progressTracker = new();
        processDisplay = GetComponent<ProcessDisplay>();
        dangerAlert = GetComponent<ProcessDangerAlert>();
    }

    private void Update()
    {
        if (!isHot) return;
        if (containerSlots.Count <= 0) return;
        if (cookwareState == CookwareState.Empty || cookwareState == CookwareState.Burnt) return;

        progressTracker.Tick(Time.deltaTime);
        currentProcessView?.Tick(progressTracker.ProgressRatio);
        //processDisplay.Tick(progressTracker.ProgressRatio);

        if (progressTracker.IsFinished)
        {
            switch (cookwareState)
            {
                case CookwareState.Processing:

                    foreach (var slot in containerSlots)
                    {
                        slot.IngredientItem.PreparationStage = slot.ProcessSO.ToStage;
                        slot.IngredientItem.UpdateMesh(slot.ProcessSO.OutputMesh);
                    }

                    progressTracker.Set(cookwareItemSO.TotalBurnDuration);
                    processDisplay.Hide(); //??
                    currentProcessView.Hide();
                    currentProcessView = dangerAlert;
                    currentProcessView.Initialize();
                    cookwareState = CookwareState.Burning;
                    break;

                case CookwareState.Burning:

                    foreach (var slot in containerSlots)
                    {
                        slot.IngredientItem.PreparationStage = PreparationStage.Burnt;
                    }

                    progressTracker.Reset();
                    cookwareState = CookwareState.Burnt;
                    currentProcessView.Hide();
                    currentProcessView = null;
                    break;

                default:
                    break;
            }
        }
    }

    public override bool CanReceive(IPickable pickable)
    {
        if (cookwareState == CookwareState.Burnt) return false;
        if (containerSlots.Count >= containerSlots.Capacity) return false;
        if (!pickable.Transform.TryGetComponent(out IngredientItem ingredientItem)) return false;
        if (!ingredientItem.Transform.TryGetComponent(out IngredientProcessModule module)) return false;
        if (!module.TryGetProcessSO(cookwareItemSO.ProcessType, out ProcessSO _)) return false;

        return true;
    }

    public override void Receive(IPickable pickable)
    {
        if (!pickable.Transform.TryGetComponent(out IngredientItem ingredientItem)) return;
        if (!ingredientItem.Transform.TryGetComponent(out IngredientProcessModule module)) return;
        if (!module.TryGetProcessSO(cookwareItemSO.ProcessType, out ProcessSO process)) return;

        ContainerSlot slot = new(ingredientItem, module, process);

        float elapsedTime = 0f;
        float requiredTime = 0f;

        switch (cookwareState)
        {
            case CookwareState.Empty:
                containerSlots.Add(slot);
                SetItem(pickable);
                requiredTime = containerSlots.Count * cookwareItemSO.ProcessTime;
                cookwareState = CookwareState.Processing;
                break;

            case CookwareState.Processing:
                containerSlots.Add(slot);
                SetItem(pickable);
                elapsedTime = progressTracker.Current;
                requiredTime = progressTracker.Target + cookwareItemSO.ProcessTime;
                break;

            case CookwareState.Burning:
                elapsedTime = containerSlots.Count * cookwareItemSO.ProcessTime;
                requiredTime = elapsedTime + cookwareItemSO.ProcessTime;
                containerSlots.Add(slot);
                SetItem(pickable);
                cookwareState = CookwareState.Processing;
                break;

            default:
                break;
        }

        ingredientItem.IconDisplay.Hide();
        iconDisplay.Set(ingredientItem.IngredientItemSO.Icon);
        iconDisplay.Show();

        progressTracker.Set(elapsedTime, requiredTime);

        currentProcessView?.Hide();
        currentProcessView = processDisplay;
        processDisplay.Initialize();
        //processDisplay.Tick(progressTracker.ProgressRatio);
    }

    public override bool CanImportItems(List<ContainerSlot> items)
    {
        return false;

        /*
        if (cookwareState == CookwareState.Burnt) return false;
        if (containerSlots.Count + items.Count > containerSlots.Capacity) return false;
        return true;
        */
    }

    public override void ImportItems(List<ContainerSlot> items)
    {

    }

    public override bool CanExportItems()
    {
        if (cookwareState == CookwareState.Empty) return false;
        if (containerSlots.Count < containerItemSO.Capacity) return false;

        return true;
    }

    public override List<ContainerSlot> ExportItems()
    {
        var temp = new List<ContainerSlot>(containerSlots);
        cookwareState = CookwareState.Empty;
        containerSlots.Clear();
        progressTracker.Reset();
        currentProcessView.Hide();
        currentProcessView = null;
        iconDisplay.Hide();
        return temp;
    }
}