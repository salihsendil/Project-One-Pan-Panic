public struct IngredientAddedToContainerSignal
{
    public GameplayEvent GameplayEvent;
    public ItemType ItemType;

    public IngredientAddedToContainerSignal(GameplayEvent gameplayEvent, ItemType itemType)
    {
        GameplayEvent = gameplayEvent;
        ItemType = itemType;
    }
}