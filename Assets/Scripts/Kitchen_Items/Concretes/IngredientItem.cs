using UnityEngine;

[RequireComponent(typeof(ItemCanvasBillboard))]
[RequireComponent(typeof(IngredientIconDisplay))]
public class IngredientItem : MonoBehaviour, IPickable, IPoolable
{
    [SerializeField] private ItemStage itemStage;
    [SerializeField] private IngredientItemSO ingredientData;
    [SerializeField] private IngredientIconDisplay iconDisplay;

    [SerializeField] private MeshFilter meshFilter;

    [SerializeField] protected bool isPickable = true;



    public ItemStage ItemStage => itemStage;
    public IngredientItemSO IngredientData => ingredientData;
    public ItemType GetPoolType => ingredientData.ItemType;


    #region IPickable

    public Transform Transform => transform;

    public GameObject GetGameObject => gameObject;

    public ItemType GetItemType() => ingredientData.ItemType;

    public bool IsPickable { get => isPickable; set => isPickable = value; }

    #endregion


    private void Awake()
    {
        if (meshFilter == null)
            meshFilter = GetComponentInChildren<MeshFilter>();

        iconDisplay = GetComponent<IngredientIconDisplay>();
        itemStage = ingredientData.InitialStage;
        iconDisplay.SetIcon(ingredientData.Icon);
        iconDisplay.Hide();
    }

    #region IPoolable

    public void Spawn()
    {
        itemStage = ingredientData.InitialStage;

        if (itemStage == ItemStage.Instant) iconDisplay.Show();
    }

    public void Despawn()
    {
        iconDisplay.Hide();
        UpdateMesh(ingredientData.InitialMesh);
    }

    #endregion

    protected void UpdateMesh(Mesh newMesh)
    {
        if (meshFilter == null) return;
        meshFilter.sharedMesh = newMesh;
    }

    public void ItemProcessed(Mesh newMesh, ItemStage newStage)
    {
        iconDisplay.Show();
        itemStage = newStage;
        UpdateMesh(newMesh);
    }

    public void OnAddedToContainer()
    {
        iconDisplay.Hide();
    }
}