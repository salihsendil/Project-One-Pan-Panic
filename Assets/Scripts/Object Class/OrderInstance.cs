using System;

[System.Serializable]
public class OrderInstance
{
    public RecipeSO RecipeSO;
    public float RemainingTime;
    public bool HasOrderExpired = true;

    public event Action<OrderInstance> OnOrderExpired;

    public void SetOrderInstance(RecipeSO recipe)
    {
        RecipeSO = recipe;
        RemainingTime = recipe.preparationTime;
        HasOrderExpired = false;
    }

    public void Tick(float deltaTime)
    {
        RemainingTime -= deltaTime;

        if (RemainingTime <= 0)
        {
            OnOrderExpired?.Invoke(this);
        }
    }

    public void ClearOrderInstance()
    {
        RecipeSO = null;
        RemainingTime = 0;
        HasOrderExpired = true;
    }

}
