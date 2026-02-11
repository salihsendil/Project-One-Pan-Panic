using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New OrderConfigSO", menuName = "Scriptable Objects/New OrderConfigSO")]
public class OrderConfigSO : ScriptableObject
{
    public List<RecipeSO> RecipeList = new();
    public int MaxActiveOrderCount;
    public float OrderSpawnDelay;

    [TextArea]
    [SerializeField] private string Description;
}
