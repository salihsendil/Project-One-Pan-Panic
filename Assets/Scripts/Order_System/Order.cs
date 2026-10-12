using System;

[Serializable]
public class Order
{
    public int ID;
    public RecipeSO Recipe;

    private Timer timer;

    public event Action OnOrderSetAgain;

    public Order(int id, RecipeSO recipe)
    {
        ID = id;
        Recipe = recipe;
        timer.Set(recipe.PreparationTime);
    }

    public bool IsExpired => timer.IsFinished;
    public float RemainingRatio => timer.RemainingRatio;

    public void Tick(float deltaTime)
    {
        timer.Tick(deltaTime);
    }

    public void SetOrderAgain()
    {
        timer.Set(Recipe.PreparationTime);
        OnOrderSetAgain?.Invoke();
    }
}