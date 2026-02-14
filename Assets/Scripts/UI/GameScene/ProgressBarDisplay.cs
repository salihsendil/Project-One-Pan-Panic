using UnityEngine;
using UnityEngine.UI;

public class ProgressBarDisplay : MonoBehaviour
{
    [SerializeField] private Slider slider;

    private void Awake()
    {
        if (slider == null) { TryGetComponent(out slider); }
    }

    public void SetProgress(float duration)
    {
         slider.value = slider.maxValue = duration;

    }

    public void UpdateTimer(float remaining)
    {
        slider.value = remaining;
    }
}
