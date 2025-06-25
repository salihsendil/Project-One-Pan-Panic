using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class OrderGenerator : MonoBehaviour
{
    [Inject] private GameManager gameManager;

    public RecipeSO GenerateRandomRecipe()
    {
        if (gameManager.GameConfig.RecipeList.Count <= 0) { return null; }

        return gameManager.GameConfig.RecipeList[Random.Range(0, gameManager.GameConfig.RecipeList.Count)];
    }
}
