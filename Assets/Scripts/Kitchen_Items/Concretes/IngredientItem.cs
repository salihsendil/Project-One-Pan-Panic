using System;
using UnityEngine;


// Main class for all ingredients.
// It provides ingredient data, displays the UI icon, and updates the mesh.
//
// When the preparation stage is updated externally, the displayed icon is updated accordingly.
// The Icon Display getter is available so the icon can be hidden when the ingredient is added to a container.
//
// When an ingredient is processed, its mesh must be updated according to the process rule.
// This class does not handle any processing logic; it only stores the current preparation stage.


public class IngredientItem : MonoBehaviour, IPickable, IPoolable
{
    //References
    private IItemIconDisplay iconDisplay;

    [Header("State")]
    [SerializeField] private bool isPickable = true;

    [Header("Data")]
    [SerializeField] private IngredientItemSO ingredientItemSO;

    [Header("Process Stage")]
    [SerializeField] private PreparationStage preparationStage;

    [Header("Visual")]
    [SerializeField] private MeshFilter meshFilter;
    [SerializeField] private MeshRenderer meshRenderer;


    public bool IsPickable { get => isPickable; set => isPickable = value; }
    public Transform Transform => transform;


    public IngredientItemSO IngredientItemSO => ingredientItemSO;

    public PreparationStage PreparationStage
    {
        get => preparationStage;

        set
        {
            if (preparationStage == value) return;
            preparationStage = value;
            OnPreparationStageChanged();
        }
    }

    public IItemIconDisplay IconDisplay => iconDisplay;

    public ItemType ItemType => ingredientItemSO.ItemType;

    public GameObject GameObject => gameObject;

    public event Action<IngredientItem> OnItemProcessed;


    private void Awake()
    {
        iconDisplay = GetComponent<IItemIconDisplay>();
        iconDisplay.Set(ingredientItemSO.Icon);

        //meshFilter = GetComponent<MeshFilter>();
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();

        PreparationStage = ingredientItemSO.InitialStage;
    }

    private void OnPreparationStageChanged()
    {
        switch (preparationStage)
        {
            case PreparationStage.Raw:
            case PreparationStage.Burnt:
                iconDisplay.Hide();
                break;

            default:
                iconDisplay.Show();
                OnItemProcessed?.Invoke(this); ////for tutorial
                break;
        }
    }

    public void UpdateVisual(Mesh newMesh, Material newMat)
    {
        if (newMesh != null)
            meshFilter.sharedMesh = newMesh;

        if (newMesh != null)
            meshRenderer.sharedMaterial = newMat;
    }

    public void Spawn()
    {
        UpdateVisual(ingredientItemSO.InitialMesh, ingredientItemSO.InitialMaterial);
        PreparationStage = ingredientItemSO.InitialStage;
    }

    public void Despawn() { }
}