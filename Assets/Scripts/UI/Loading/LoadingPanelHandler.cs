using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using System;

public class LoadingPanelHandler : MonoBehaviour
{
    //Visibility
    [SerializeField] private CanvasGroup canvasGroup;

    //Icon - Animation
    [SerializeField] private Image animatedIcon;
    private Sequence iconSequence;

    //Fade Time
    [SerializeField] private float fadeOutTime = 0.4f;

    private void Awake()
    {
        canvasGroup.alpha = 0;
        SetInteractable(false);
    }

    private void SetInteractable(bool isVisible)
    {
        canvasGroup.blocksRaycasts = isVisible;
        canvasGroup.interactable = isVisible;
    }

    private void AnimateIcon()
    {
        RectTransform transform = animatedIcon.rectTransform;

        iconSequence?.Kill();

        iconSequence = DOTween.Sequence();
        iconSequence.Append(transform.DOAnchorPosY(transform.anchoredPosition.y + 30, 0.5f).SetEase(Ease.OutQuad));
        iconSequence.AppendInterval(0.05f);
        iconSequence.Append(transform.DOAnchorPosY(transform.anchoredPosition.y, 0.5f).SetEase(Ease.InQuad));
        iconSequence.AppendInterval(0.05f);
        iconSequence.SetLoops(-1);
    }

    public void SetCanvasVisibility(bool isVisible, Action onComplete)
    {
        SetInteractable(isVisible);

        int fade;
        if (isVisible)
        {
            fade = 1;
            AnimateIcon();
        }

        else
        {
            fade = 0;
            iconSequence?.Kill();
        }

        canvasGroup.DOFade(fade, fadeOutTime).SetEase(Ease.InOutSine).OnComplete(() =>
        {
            onComplete?.Invoke();
        });
    }
}
