public interface IContainer
{
    public bool CanAddItem(IPickable pickable, out IngredientItem ingredient);
    public void AddItem(IngredientItem ingredient);
}
