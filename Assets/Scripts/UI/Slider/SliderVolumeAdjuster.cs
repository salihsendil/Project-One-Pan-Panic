using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Slider))]
public class SliderVolumeAdjuster : MonoBehaviour, IPointerUpHandler, IDragHandler
{
    [Inject] private AudioService audioService;

    [SerializeField] private AudioType audioType;
    [SerializeField] private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
        SetSliderValue(audioService.GetVolume(audioType));
    }

    private void ChangeValue()
    {
        audioService.UpdateVolume(audioType, slider.value);
    }
    public void SetSliderValue(float value)
    {
        slider.value = Mathf.Max(value, 0.00001f);
    }

    public void OnDrag(PointerEventData eventData)
    {
        ChangeValue();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        audioService.SaveData();
    }
}
