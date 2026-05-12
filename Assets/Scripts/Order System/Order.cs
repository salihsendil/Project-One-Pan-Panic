using System;

[Serializable]
public class Order
{
    public int OrderID;
    public RecipeSO Recipe;
    public float Time;

    public event Action OnOrderSetAgain;

    public Order(int orderID, RecipeSO recipe)
    {
        OrderID = orderID;
        Recipe = recipe;
        Time = recipe.PreparationTime;
    }

    public bool IsExpired => Time <= 0f;

    public void Tick(float deltaTime)
    {
        Time -= deltaTime;
    }

    public void SetOrderAgain()
    {
        Time = Recipe.PreparationTime;
        OnOrderSetAgain?.Invoke();
    }
}
