using UnityEngine;

public class OrderInstance
{
    public RecipeSO RecipeSO;
    public float RemainingTime;

    public void SetOrderInstance(RecipeSO recipe)
    {
        RecipeSO = recipe;
        RemainingTime = recipe.preparationTime;
    }

    public void Tick(float deltaTime)
    {
        RemainingTime -= deltaTime;
    }

}
