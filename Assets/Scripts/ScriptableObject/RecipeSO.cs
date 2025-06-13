using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Recipe", menuName ="Scriptable Object/New Recipe")]
public class RecipeSO : ScriptableObject
{
    public string recipeName;
    [SerializeField] public List<KitchenItemSO> recipeList = new List<KitchenItemSO>();
}
