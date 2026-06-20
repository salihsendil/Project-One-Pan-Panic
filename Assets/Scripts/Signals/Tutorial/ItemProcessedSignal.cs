public struct ItemProcessedSignal
{
    public GameplayEvent GameplayEventType;
    public ItemType ItemType;
    public ProcessType ProcessType;

    public ItemProcessedSignal(GameplayEvent gameplayEventType, ItemType ıtemType, ProcessType processType)
    {
        GameplayEventType = gameplayEventType;
        ItemType = ıtemType;
        ProcessType = processType;
    }
}