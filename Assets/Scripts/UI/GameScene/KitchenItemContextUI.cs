using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;


public class KitchenItemContextUI : MonoBehaviour
{
    [Inject] private SignalBus signalBus;

    [SerializeField] private List<Image> iconList = new();

    private void Awake()
    {
        ClearItems();
    }

    private void OnEnable()
    {
        signalBus.Subscribe<DisplayItemContextSignal>(SetIcons);
        signalBus.Subscribe<ClearDisplayItemsSignal>(ClearItems);
    }

    private void OnDisable()
    {
        signalBus.Unsubscribe<DisplayItemContextSignal>(SetIcons);
        signalBus.Unsubscribe<ClearDisplayItemsSignal>(ClearItems);
    }

    private void SetIcons(DisplayItemContextSignal signal)
    {
        ClearItems();

        for (int i = 0; i < signal.icons.Count; i++)
        {
            iconList[i].enabled = true;
            iconList[i].sprite = signal.icons[i];
        }
    }

    public void ClearItems()
    {
        foreach (var icon in iconList)
        {
            icon.enabled = false;
        }
    }
}
