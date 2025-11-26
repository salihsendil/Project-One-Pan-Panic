

public class CookableBehaviour : BaseItemBehaviour
{
    public override ProcessType GetProcessType() => ProcessType.Cook;

    public override WorkStage GetWorkStage() => WorkStage.Cooking;
}
