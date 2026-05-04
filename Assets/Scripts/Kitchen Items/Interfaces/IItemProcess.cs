using System;

public interface IItemProcess
{
    public ProcessStage ProcessStage { get; }
    public ProcessRuleSO ProcessRule { get; }

    public event Action<IItemProcess> OnProcessFinished;

    public bool CanProcess(ProcessType processType, ItemStage stage);
    public void StartProcess();
    public void TickProcess(float delta);
    public void PauseProcess();
    public void FinishProcess();
}
