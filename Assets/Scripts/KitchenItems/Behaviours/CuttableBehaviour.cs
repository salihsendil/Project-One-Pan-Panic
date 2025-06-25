using System;
using System.Collections;
using UnityEngine;

public class CuttableBehaviour : MonoBehaviour, ICuttableItem, IKitchenItemStateProvider
{
    [SerializeField] private KitchenItem kitchenItem;
    [SerializeField] private KitchenItemState currentState = KitchenItemState.Whole;
    [SerializeField] private float cutDuration;
    [SerializeField] private float cutProgress;
    [SerializeField] private Coroutine cuttingCoroutine;

    public KitchenItemState CurrentState { get => currentState; set => currentState = value; }

    private void Awake()
    {
        TryGetComponent(out kitchenItem);
    }

    public void StartCut(KitchenItemSO.ProcessRule processRule, ITransferItemHandler player, IStationTimerDisplayer timerDisplayer)
    {
        if (cuttingCoroutine != null) { return; }

        cutProgress = 0f;
        cutDuration = processRule.processTime;
        player.HasBusyForProcess = true;
        timerDisplayer.SetTimer(cutDuration);
        cuttingCoroutine = StartCoroutine(CuttingProgress(processRule, player, timerDisplayer));
    }

    public IEnumerator CuttingProgress(KitchenItemSO.ProcessRule processRule, ITransferItemHandler player, IStationTimerDisplayer timerDisplayer)
    {
        while (cutProgress < cutDuration)
        {
            cutProgress += Time.deltaTime;
            timerDisplayer.UpdateTimerSlider(cutProgress);
            yield return null;
        }

        OnCutComplete(processRule, player, timerDisplayer);
    }

    public void OnCutComplete(KitchenItemSO.ProcessRule processRule, ITransferItemHandler player, IStationTimerDisplayer timerDisplayer)
    {
        StopCoroutine(cuttingCoroutine);
        timerDisplayer.DisableTimer();
        kitchenItem.UpdateVisual(processRule.outputMesh);
        currentState = processRule.outputState;
        cuttingCoroutine = null;
        kitchenItem.IsProcessed = true;
        player.HasBusyForProcess = false;
    }
}
