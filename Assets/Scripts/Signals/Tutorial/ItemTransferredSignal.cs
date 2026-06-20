public struct ItemTransferredSignal
{
    public GameplayEvent GameplayEventType;
    public ItemType ItemType;
    public IInteractor From;
    public IInteractor To;

    public ItemTransferredSignal(GameplayEvent gameplayEventType, ItemType itemType, IInteractor from, IInteractor to)
    {
        GameplayEventType = gameplayEventType;
        ItemType = itemType;
        From = from;
        To = to;
    }
}