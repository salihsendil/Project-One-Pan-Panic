using System.Collections.Generic;
using UnityEngine;

public class OrderGenerator : MonoBehaviour
{
    [SerializeField] private List<RecipeSO> recipeList = new List<RecipeSO>();

    public RecipeSO GenerateRandomRecipe()
    {
        Debug.Log($"capacity: {recipeList.Capacity} ");
        Debug.Log($"count: {recipeList.Count} ");
        return recipeList[Random.Range(0, recipeList.Capacity)];
    }

}
