using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CounterSwapManager : MonoBehaviour
{
    private Coroutine swapCoroutine;
    private WaitForSeconds swapInterval;
    private List<BaseCounter> counters = new();

    [Header("Swap Size")]
    [SerializeField] private int swapCounterSize = 5;

    [Header("Time Interval")]
    [SerializeField] [Min(5)] private int swapTimeInterval = 7;

    [Header("Tween")]
    [SerializeField] private float hideShowTweenDuration = 0.5f;
    [SerializeField] private float appendCallbackDelay = 1f;

    [Header("Total Time Interval")]
    [SerializeField]
    [ReadOnly]
    [Tooltip("The total time between swaps, including wait times and animation times.")]
    private float totalTimeInterval;

    private void OnValidate()
    {
        totalTimeInterval = swapTimeInterval + hideShowTweenDuration * 2 + appendCallbackDelay;
    }

    private void OnEnable()
    {

        BaseCounter[] temp = FindObjectsByType<BaseCounter>(FindObjectsInactive.Exclude, FindObjectsSortMode.None); //CHANGE EXCLUDE TO INCLUDE WHEN THE TEST FINISH!!

        foreach (var item in temp)
        {
            if (item is not DeliveryCounter)
            {
                counters.Add(item);
            }
        }
    }

    private void OnDisable()
    {
        if (swapCoroutine != null)
        {
            StopCoroutine(swapCoroutine);
            swapCoroutine = null;
        }
    }

    private void Start()
    {
        swapInterval = new(totalTimeInterval);
        swapCoroutine = StartCoroutine(SwapCounters());
    }

    private IEnumerator SwapCounters()
    {
        while (true)
        {
            yield return swapInterval;
            ShuffleList();
            StartSwapSequence();
        }
    }

    private void ShuffleList()
    {
        for (int i = 0; i < counters.Count; i++)
        {
            int random = Random.Range(i, counters.Count);
            BaseCounter tempCounter = counters[i];
            counters[i] = counters[random];
            counters[random] = tempCounter;
        }
    }

    private void StartSwapSequence()
    {
        Sequence sequence = DOTween.Sequence();

        for (int i = 0; i < swapCounterSize; i++)
        {
            sequence.Join(
                counters[i].transform
                    .DOScale(Vector3.zero, hideShowTweenDuration)
                    .SetEase(Ease.InBack)
            );
        }

        sequence.AppendCallback(() => SetCounterActive(false));
        sequence.AppendCallback(SwapCountersPositions);
        sequence.AppendInterval(appendCallbackDelay);
        sequence.AppendCallback(() => SetCounterActive(true));

        for (int i = 0; i < swapCounterSize; i++)
        {
            sequence.Join(
                counters[i].transform
                    .DOScale(Vector3.one, hideShowTweenDuration)
                    .SetEase(Ease.OutBack)
            );
        }
    }

    private void SetCounterActive(bool isActive)
    {
        for (int i = 0; i < swapCounterSize; i++)
            counters[i].gameObject.SetActive(isActive);
    }

    private void SwapCountersPositions()
    {
        for (int i = 0; i < swapCounterSize; i++)
        {
            int random = Random.Range(i, swapCounterSize);
            Vector3 tempCounterPos = counters[i].transform.position;
            counters[i].transform.position = counters[random].transform.position;
            counters[random].transform.position = tempCounterPos;
        }
    }
}