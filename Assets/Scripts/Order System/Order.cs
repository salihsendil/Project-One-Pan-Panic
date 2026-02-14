public class Order
{
    public int ID { get; }
    public RecipeSO Recipe { get; }
    public float RemainingTime { get; private set; }

    public Order(int id, RecipeSO recipe)
    {
        ID = id;
        Recipe = recipe;
        RemainingTime = recipe.PreperationTime;
    }


    public bool IsExpired()
    {
        return RemainingTime <= 0;
    }

    public void TickTime(float deltaTime)
    {
        RemainingTime -= deltaTime;
    }
}