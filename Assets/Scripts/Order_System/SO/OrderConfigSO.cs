using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New_OrderConfigSO", menuName = "Scriptable Objects/Order/New OrderConfigSO")]

public class OrderConfigSO : ScriptableObject
{
    //public RecipeSO firstOrder;

    [Header("Settings")]
    [Range(1, 5), Tooltip("Max amount of active order count.")] public int MaxOrderCount;
    [Min(3), Tooltip("Order frequency in seconds. \nMin value: 3 seconds")] public float OrderSpawnInterval;

    [Header("Recipe List")]
    public List<RecipeSO> Recipes = new();
}
