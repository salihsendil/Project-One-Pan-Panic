using UnityEngine;

[CreateAssetMenu(fileName = "New IngredientItemSO", menuName = "Scriptable Objects/Kitchen Items/New IngredientItemSO")]
public class IngredientItemSO : ScriptableObject
{
    [Header("Name")]
    public string Name;
    
    [Header("Initial State")]
    public Mesh InitialMesh;
    public ItemStage InitialStage;


    [Header("Visual")]
    public Sprite Icon;
    public GameObject Prefab;


    [Header("Pool")]
    public ItemType ItemType;
}