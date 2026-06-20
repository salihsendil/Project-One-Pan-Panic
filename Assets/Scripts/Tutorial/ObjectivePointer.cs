using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ObjectivePointer : MonoBehaviour
{
    [Header("Positioning")]
    [SerializeField] private Vector3 offset;

    [Header("Visual")]
    [SerializeField] private Image iconImage;

    //Tween
    [Header("Tween")]
    [SerializeField] private float moveAmount = 0.25f;
    [SerializeField] private float duration = 0.5f;

    private void Awake()
    {
        HidePointer();
    }

    private void StartTween()
    {
        transform.DOKill();
        transform.DOMoveY(transform.position.y + moveAmount, duration)
                            .SetEase(Ease.InOutSine)
                            .SetLoops(-1, LoopType.Yoyo);
    }

    public void SetPointer(Vector3 targetPos, Sprite tutorialIcon = null)
    {
        gameObject.SetActive(true);
        transform.position = targetPos + offset;

        iconImage.enabled = tutorialIcon != null;
        iconImage.sprite = tutorialIcon;

        StartTween();
    }

    public void HidePointer()
    {
        transform.DOKill();
        gameObject.SetActive(false);
    }
}
