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

    public void StartCut(KitchenItemSO.ProcessRule processRule, ITransferItemHandler player)
    {
        if (cuttingCoroutine != null) { return; }

        cutProgress = 0f;
        cutDuration = processRule.processTime;
        Debug.Log("kesme ba�lad�.");
        player.HasBusyForProcess = true;
        // ui g�ster
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
        kitchenItem.IsProcessed = true;
        player.HasBusyForProcess = false;
    }
}
