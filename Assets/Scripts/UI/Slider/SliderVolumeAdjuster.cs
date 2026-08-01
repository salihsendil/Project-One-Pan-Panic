using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Slider))]
public class SliderVolumeAdjuster : MonoBehaviour,IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [Inject] private GameSettingsService gameSettingsService;

    [SerializeField] private AudioType audioType;
    [SerializeField] private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
        SetSliderValue(gameSettingsService.GetVolume(audioType));
    }

    private void ChangeValue()
    {
        gameSettingsService.UpdateVolume(audioType, slider.value);
    }
    public void SetSliderValue(float value)
    {
        slider.value = Mathf.Max(value, 0.00001f);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ChangeValue();
    }

    public void OnDrag(PointerEventData eventData)
    {
        ChangeValue();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        gameSettingsService.SaveData();
    }
}
