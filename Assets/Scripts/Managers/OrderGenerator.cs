using System.Collections.Generic;
using UnityEngine;

public class OrderGenerator : MonoBehaviour
{
    [SerializeField] private List<RecipeSO> recipeList = new List<RecipeSO>();

    public RecipeSO GenerateRandomRecipe()
    {
        if (recipeList.Count <= 0) { return null; }

        return recipeList[Random.Range(0, recipeList.Capacity)];
    }

}
