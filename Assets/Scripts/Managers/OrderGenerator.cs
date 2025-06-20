using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class OrderGenerator : MonoBehaviour
{
    [Inject] private GameManager gameManager;

    public RecipeSO GenerateRandomRecipe()
    {
        if (gameManager.Settings.RecipeList.Count <= 0) { return null; }

        return gameManager.Settings.RecipeList[Random.Range(0, gameManager.Settings.RecipeList.Count)];
    }
}
