using System.Collections;
using UnityEngine;

public class CookableBehaviour : MonoBehaviour, ICookableItem, IKitchenItemStateProvider
{
    [SerializeField] private KitchenItem kitchenItem;
    [SerializeField] private KitchenItemState currentState = KitchenItemState.Raw;
    [SerializeField] private float cookDuration;
    [SerializeField] private float cookProgress;
    [SerializeField] private Coroutine cookingCoroutine;

    public KitchenItemState CurrentState { get => currentState; set => currentState = value; }

    private void Awake()
    {
        TryGetComponent(out kitchenItem);
    }

    public void StartCook(KitchenItemSO.ProcessRule processRule, IStationTimerDisplayer timerDisplayer)
    {
        if (cookingCoroutine != null) { return; }

        cookProgress = 0f;
        cookDuration = processRule.processTime;
        timerDisplayer.SetTimer(cookDuration);
        // karakteri kitle
        // ui göster
        // animation state machine baþlat
        cookingCoroutine = StartCoroutine(CookingProgress(processRule, timerDisplayer));
    }
    public IEnumerator CookingProgress(KitchenItemSO.ProcessRule processRule, IStationTimerDisplayer timerDisplayer)
    {
        while (cookProgress < cookDuration)
        {
            cookProgress += Time.deltaTime;
            timerDisplayer.UpdateTimerSlider(cookProgress);
            yield return null;
        }

        OnCookComplete(processRule, timerDisplayer);
    }
    public void OnCookComplete(KitchenItemSO.ProcessRule processRule, IStationTimerDisplayer timerDisplayer)
    {
        StopCoroutine(cookingCoroutine);
        timerDisplayer.DisableTimer();
        cookingCoroutine = null;
        kitchenItem.UpdateVisual(processRule.outputMesh);
        currentState = processRule.outputState;
        kitchenItem.IsProcessed = true;

        if (kitchenItem.KitchenItemData.GetProcessRuleMatch(currentState, out KitchenItemSO.ProcessRule rule))
        {
            StartCook(rule, timerDisplayer);
        }
    }

    public void CancelCook(IStationTimerDisplayer timerDisplayer)
    {
        if (cookingCoroutine != null)
        {
            StopCoroutine(cookingCoroutine);
            cookingCoroutine = null;
        }
        timerDisplayer.DisableTimer();

    }
}
