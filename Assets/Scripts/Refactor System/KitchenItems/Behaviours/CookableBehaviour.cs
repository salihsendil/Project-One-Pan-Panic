using System.Collections;
using UnityEngine;

public class CookableBehaviour : MonoBehaviour, ICookableItem
{
    [SerializeField] private CookingState currentState = CookingState.Raw;
    [SerializeField] private float cookDuration;
    [SerializeField] private float cookProgress;
    [SerializeField] private Coroutine cookingCoroutine;

    public CookingState CurrentState { get => currentState; set => currentState = value; }

    public void StartCook()
    {
        cookDuration = GetComponent<KitchenItem>().KitchenItemData.processTime;
        cookProgress = 0f;
        Debug.Log("pi�irme ba�lad�.");
        // karakteri kitle
        // ui g�ster
        // animation state machine ba�lat
        //OnCutProgresStarted?.Invoke();
        cookingCoroutine = StartCoroutine(CookingProgress());
    }
    public IEnumerator CookingProgress()
    {
        while (cookProgress < cookDuration)
        {
            cookProgress += Time.deltaTime;
            Debug.Log(cookProgress);
            //OnCutProgressChanged?.Invoke(cutProgress);
            yield return null;
        }

        OnCookComplete();
    }
    public void OnCookComplete()
    {
        Debug.Log("pi�irme bitti");
        StopCoroutine(cookingCoroutine);
        currentState = CookingState.Cooked;
        //OnCutProgressEnded?.Invoke();
    }
}
