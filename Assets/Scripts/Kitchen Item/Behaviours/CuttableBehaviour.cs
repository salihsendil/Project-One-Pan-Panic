

public class CuttableBehaviour : BaseItemBehaviour
{
    public override ProcessType GetProcessType() => ProcessType.Cut;

    public override WorkStage GetWorkStage() => WorkStage.Cutting;
}
