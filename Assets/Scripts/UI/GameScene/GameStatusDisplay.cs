using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class GameStatusDisplay : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private TMP_Text statusText;

    [Header("Tween")]
    private Sequence sequence;
    [SerializeField] private float firstScaleDuration = 0.12f;
    [SerializeField] private float secondScaleDuration = 1.28f;
    [SerializeField] private Ease scaleEase = Ease.InOutSine;
    [SerializeField] private Ease fadeEase = Ease.InOutQuint;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
        statusText = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        sequence = DOTween.Sequence().SetAutoKill(false).Pause();
        sequence.Append(rectTransform.DOScale(Vector3.one, firstScaleDuration).SetEase(scaleEase));
        sequence.Join(statusText.DOFade(1f, firstScaleDuration).SetEase(fadeEase));
        sequence.Append(rectTransform.DOScale(Vector3.one * 1.1f, secondScaleDuration).SetEase(scaleEase));
    }

    public void Show(string msg)
    {
        if (canvasGroup.alpha == 0)
            canvasGroup.alpha = 1;
        statusText.text = msg;
        Animate();
    }

    public IEnumerator ShowCountdown(string msg1, string msg2, float totalDisplayTime)
    {
        Show(msg1);
        yield return new WaitForSeconds(totalDisplayTime - 1);
        Show(msg2);
        yield return new WaitForSeconds(1);
    }

    public void Hide()
    {
        canvasGroup.alpha = 0;
    }

    private void Animate()
    {
        statusText.alpha = 0f;
        rectTransform.localScale = Vector3.zero;
        sequence.Restart();
    }
}