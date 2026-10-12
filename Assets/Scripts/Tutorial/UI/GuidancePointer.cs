using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(Image))]
public class GuidancePointer : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] private CanvasGroup canvasGroup;

    [Header("Positioning")]
    [SerializeField] private Vector3 offset = new(0, 0.75f, 0.5f);

    [Header("Visual")]
    [SerializeField] private Image iconImage;

    [Header("Tween")]
    [SerializeField] private float moveAmount = 0.25f;
    [SerializeField] private float duration = 0.5f;

    private void Awake()
    {
        iconImage = GetComponent<Image>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;

        transform.localPosition += offset;
    }

    private void StartTween()
    {
        transform.DOKill();
        transform.DOMoveY(transform.position.y + moveAmount, duration)
                            .SetEase(Ease.InOutSine)
                            .SetLoops(-1, LoopType.Yoyo);
    }

    public void Show(Sprite tutorialIcon = null)
    {
        canvasGroup.DOFade(1f, 0.2f).SetEase(Ease.InOutSine);

        iconImage.enabled = tutorialIcon != null;
        iconImage.sprite = tutorialIcon;

        StartTween();
    }

    public void Hide()
    {
        canvasGroup.DOFade(0f, 0.2f).SetEase(Ease.InOutSine);
        transform.DOKill();
    }
}
