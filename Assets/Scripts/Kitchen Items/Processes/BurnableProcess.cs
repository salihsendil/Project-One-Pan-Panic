using UnityEngine;

[RequireComponent(typeof(WarningIconDisplay))]
public class BurnableProcess : BaseItemProcess
{
    private void Awake()
    {
        processDisplayer = GetComponent<WarningIconDisplay>();
    }

    public override void StartProcess()
    {
        base.StartProcess();
        processDisplayer.Initialize();
    }

    public override void PauseProcess()
    {
        base.PauseProcess();
        processDisplayer.Disable();
    }
}
