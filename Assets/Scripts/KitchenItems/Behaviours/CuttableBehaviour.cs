using System.Collections;
using UnityEngine;

public class CuttableBehaviour : MonoBehaviour, ICuttableItem
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

    public void StartCut(KitchenItemSO.ProcessRule processRule)
    {
        if (cuttingCoroutine != null) { return; }

        cutProgress = 0f;
        cutDuration = processRule.processTime;
        Debug.Log("kesme baþladý.");
        // karakteri kitle
        // ui göster
        // animation state machine baþlat
        cuttingCoroutine = StartCoroutine(CuttingProgress(processRule));
    }

    public IEnumerator CuttingProgress(KitchenItemSO.ProcessRule processRule)
    {
        while (cutProgress < cutDuration)
        {
            cutProgress += Time.deltaTime;
            Debug.Log(cutProgress);
            yield return null;
        }

        OnCutComplete(processRule);
    }

    public void OnCutComplete(KitchenItemSO.ProcessRule processRule)
    {
        Debug.Log("kesme bitti");
        StopCoroutine(cuttingCoroutine);
        kitchenItem.UpdateVisual(processRule.outputMesh);
        currentState = processRule.outputState;
        cuttingCoroutine = null;
        kitchenItem.isProcessed = true;
    }
}
