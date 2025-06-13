using System.Collections;
using UnityEngine;

public class CuttableBehaviour : MonoBehaviour, ICuttableItem
{
    [SerializeField] private KitchenItem kitchenItem;
    [SerializeField] private KitchenItemState currentState = KitchenItemState.Whole;
    [SerializeField] private float cutDuration;
    [SerializeField] private float cutProgress;
    [SerializeField] private Coroutine cuttingCoroutine;

    public KitchenItemState CurrentState { get => currentState; }

    private void Awake()
    {
        TryGetComponent(out kitchenItem);
    }

    public void StartCut(KitchenItemSO.ProcessRule processRule, ITransferItemHandler player)
    {
        if (cuttingCoroutine != null) { return; }

        cutProgress = 0f;
        cutDuration = processRule.processTime;
        Debug.Log("kesme baþladý.");
        player.HasBusyForProcess = true;
        // karakteri kitle
        // ui göster
        // animation state machine baþlat
        cuttingCoroutine = StartCoroutine(CuttingProgress(processRule, player));
    }

    public IEnumerator CuttingProgress(KitchenItemSO.ProcessRule processRule, ITransferItemHandler player)
    {
        while (cutProgress < cutDuration)
        {
            cutProgress += Time.deltaTime;
            Debug.Log(cutProgress);
            yield return null;
        }

        OnCutComplete(processRule, player);
    }

    public void OnCutComplete(KitchenItemSO.ProcessRule processRule, ITransferItemHandler player)
    {
        Debug.Log("kesme bitti");
        StopCoroutine(cuttingCoroutine);
        kitchenItem.UpdateVisual(processRule.outputMesh);
        currentState = processRule.outputState;
        cuttingCoroutine = null;
        kitchenItem.isProcessed = true;
        player.HasBusyForProcess = false;
    }
}
