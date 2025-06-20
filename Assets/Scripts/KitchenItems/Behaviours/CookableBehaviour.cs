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

    public void StartCook(KitchenItemSO.ProcessRule processRule)
    {
        if (cookingCoroutine != null) { return; }

        cookProgress = 0f;
        cookDuration = processRule.processTime;
        // karakteri kitle
        // ui göster
        // animation state machine baþlat
        cookingCoroutine = StartCoroutine(CookingProgress(processRule));
    }
    public IEnumerator CookingProgress(KitchenItemSO.ProcessRule processRule)
    {
        while (cookProgress < cookDuration)
        {
            cookProgress += Time.deltaTime;
            yield return null;
        }

        OnCookComplete(processRule);
    }
    public void OnCookComplete(KitchenItemSO.ProcessRule processRule)
    {
        StopCoroutine(cookingCoroutine);
        cookingCoroutine = null;
        kitchenItem.UpdateVisual(processRule.outputMesh);
        currentState = processRule.outputState;
        kitchenItem.IsProcessed = true;

        if (kitchenItem.KitchenItemData.GetProcessRuleMatch(currentState, out KitchenItemSO.ProcessRule rule))
        {
            StartCook(rule);
        }
    }

    public void CancelCook()
    {
        if (cookingCoroutine != null)
        {
            StopCoroutine(cookingCoroutine);
            cookingCoroutine = null;
        }
    }
}
