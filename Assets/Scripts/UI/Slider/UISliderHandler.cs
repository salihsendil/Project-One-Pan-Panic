using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class UISliderHandler : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private Slider slider;
    private BaseUIAction[] uiActions;
    private BaseUIAction<float>[] uiActionsFloat;

    private void Awake()
    {
        if (slider == null) { TryGetComponent(out slider); }

        uiActions = GetComponents<BaseUIAction>();
        uiActionsFloat = GetComponents<BaseUIAction<float>>();
    }

    public void SetSliderValue(float value)
    {
        slider.value = value;
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        foreach (var actionFloat in uiActionsFloat)
        {
            if (actionFloat == null) { continue; }

            actionFloat.Execute(slider.value);
        }
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        foreach (var action in uiActions)
        {
            if (action == null) { continue; }

            action.Execute();
        }
    }
}