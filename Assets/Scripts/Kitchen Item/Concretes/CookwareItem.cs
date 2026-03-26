using UnityEngine;

public class CookwareItem : BaseKitchenItem, IContainer, ICookware
{
    public bool AddIngredient(IngredientItem ingredient)
    {
        throw new System.NotImplementedException();
    }

    public bool CanAddIngredient(IngredientItem ingredient)
    {
        throw new System.NotImplementedException();
    }

    public void Tick(float delta)
    {
        throw new System.NotImplementedException();
    }
}
