using System.Collections;
using UnityEngine;

public class CuttableBehaviours : MonoBehaviour, IItemBehaviour
{
    private ProcessTimer processTimer = new();
    private IEnumerator coroutine;
    public ProcessType GetProcessType() => ProcessType.Cut;

    public void StartProcess(KitchenItem kitchenItem, ProcessRule rule)
    {
        processTimer.SetTimer(rule.processTime);
        coroutine = TickProcess();
        StartCoroutine(coroutine);
    }

    public IEnumerator TickProcess()
    {
        while (!processTimer.IsFinished())
        {
            processTimer.TickTimer();
            Debug.Log("kalan süre: " + processTimer.Remaining);
            yield return null;
        }
        StopCoroutine(coroutine);
        FinishProcess();
    }

    public void FinishProcess()
    {
        processTimer.ResetTimer();
        Debug.Log("timer resetted: " + processTimer.Remaining);
    }

    public void CancelProcess()
    {

    }
}
