using TMPro;
using UnityEngine;

public enum BuyButtonState { Buy, Equip, Equipped }

public class BuyButtonDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text buyButtonText;

    public void UpdateButtonText(string text)
    {
        buyButtonText.text = text;
    }
}
