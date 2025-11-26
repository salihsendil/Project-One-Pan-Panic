

public class BurnableBehaviour : BaseItemBehaviour
{
    public override ProcessType GetProcessType() => ProcessType.Burn;

    public override WorkStage GetWorkStage() => WorkStage.Burning;
}
