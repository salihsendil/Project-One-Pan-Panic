using UnityEngine;

[CreateAssetMenu(fileName = "New_Ingredient_Item_SO", menuName = "Scriptable Objects/Kitchen Items/Ingredients/New Ingredient Item SO")]
public class IngredientItemSO : ScriptableObject
{
    [Header("Type")]
    public ItemType ItemType;

    [Header("Visual")]
    public GameObject Prefab;
    public Mesh InitialMesh;

    [Header("UI")]
    public Sprite Icon;

    [Header("State")]
    public PreparationStage InitialStage;   
}