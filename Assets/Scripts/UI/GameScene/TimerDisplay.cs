using UnityEngine;
using TMPro;

public class TimerDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;

    private void Awake()
    {
        timerText = GetComponent<TMP_Text>();
    }

    public void UpdateTimerText(int remaining)
    {
        int minute = remaining / 60;
        int second = remaining % 60;
        timerText.text = $"{minute:D2}:{second:D2}";
    }
}
