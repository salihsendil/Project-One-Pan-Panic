using TMPro;
using UnityEngine;

public class CurrencyDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text currencyText;

    public void UpdateCurrencyText(string curreny)
    {
        currencyText.text = curreny;
    }

}
