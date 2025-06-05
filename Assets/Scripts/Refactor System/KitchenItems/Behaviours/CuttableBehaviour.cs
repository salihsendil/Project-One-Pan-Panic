using System;
using System.Collections;
using UnityEngine;

public class CuttableBehaviour : MonoBehaviour, ICuttableItem
{
    public event Action OnCutProgresStarted;
    public event Action<float> OnCutProgressChanged;
    public event Action OnCutProgressEnded;

    [SerializeField] private CuttingState currentState = CuttingState.Whole;
    [SerializeField] private float cutDuration;
    [SerializeField] private float cutProgress;
    [SerializeField] private Coroutine cuttingCoroutine;

    public CuttingState CurrentState { get => currentState; set => currentState = value; }

    public void StartCut()
    {
        cutDuration = GetComponent<KitchenItem>().KitchenItemData.processTime;
        cutProgress = 0f;
        Debug.Log("kesme baþladý.");
        // karakteri kitle
        // ui göster
        // animation state machine baþlat
        OnCutProgresStarted?.Invoke();
        cuttingCoroutine = StartCoroutine(CuttingProgress());
    }

    public IEnumerator CuttingProgress()
    {
        while (cutProgress < cutDuration)
        {
            cutProgress += Time.deltaTime;
            Debug.Log(cutProgress);
            OnCutProgressChanged?.Invoke(cutProgress);
            yield return null;
        }

        OnCutComplete();
    }

    public void OnCutComplete()
    {
        Debug.Log("kesme bitti");
        StopCoroutine(cuttingCoroutine);
        currentState = CuttingState.Chopped;
        OnCutProgressEnded?.Invoke();
    }
}
