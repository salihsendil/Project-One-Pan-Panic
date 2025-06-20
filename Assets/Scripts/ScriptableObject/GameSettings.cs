using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Game Settings", menuName ="Scriptable Object/New Game Settings")]
public class GameSettings : ScriptableObject
{
    [Header("Order Settings")]
    public int MAX_ORDER_COUNT = 5;
    public List<RecipeSO> RecipeList = new List<RecipeSO>();
    public float FirstOrderDelay = 5f;
    public float MinOrderDelay = 12f;
    public float maxOrderDelay = 20f;

    [Header("Time")]
    public float GameTime = 400f;

}
