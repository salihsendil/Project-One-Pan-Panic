using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class WarningIconDisplay : MonoBehaviour, IProcessDisplayer
{
    // Zenject
    [Inject] private SFXService sfxService;

    // Icon Reference
    [SerializeField] private Image icon;

    // Animation Settings
    private Sequence fadeSequence;
    [SerializeField] private float duration = 1f; // Pulse Duration


    private void Awake()
    {
        icon.enabled = false;
        icon.color = Color.clear;

        fadeSequence = DOTween.Sequence();
        fadeSequence
            .Append(icon.DOColor(Color.white, duration))
            .AppendCallback(() => sfxService.PlaySFXOneShot(SFXType.IngredientBurn))
            .Append(icon.DOColor(Color.clear, duration))
            .SetLoops(-1, LoopType.Restart)
            .SetAutoKill(false)
            .Pause();
    }

    public void Initialize()
    {
        icon.enabled = true;
        StartFadeAnimation();
    }

    private void StartFadeAnimation()
    {
        fadeSequence.Restart();
    }

    public void Tick(float value)
    {
        float speedMultiplier = Mathf.Lerp(1f, 10f, value);
        fadeSequence.timeScale = speedMultiplier;
    }

    public void Disable()
    {
        fadeSequence.Pause();
        fadeSequence.Rewind();
        icon.enabled = false;
    }

    private void OnDestroy()
    {
        fadeSequence?.Kill();
    }
}