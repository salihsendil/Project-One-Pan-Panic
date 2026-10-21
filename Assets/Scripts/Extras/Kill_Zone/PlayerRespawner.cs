using System.Collections;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class PlayerRespawner : MonoBehaviour
{
    private Coroutine respawnCoroutine;

    [Header("Time")]
    [SerializeField] private Timer timer = new();
    [SerializeField] private int respawnDuration = 3;

    [Header("UI")]
    [SerializeField] private Canvas canvas;
    [SerializeField] private TMP_Text timerText;

    private void Start()
    {
        canvas.transform.rotation = Camera.main.transform.rotation;
        canvas.enabled = false;
    }

    public void Respawn(PlayerController player)
    {
        timer.Set(respawnDuration);
        canvas.enabled = true;
        UpdateText((int)timer.Remaining);

        respawnCoroutine = StartCoroutine(StartCountdown(player));
    }

    private IEnumerator StartCountdown(PlayerController player)
    {
        int remainingTime = (int)timer.Remaining;

        while (!timer.IsFinished)
        {
            timer.Tick(Time.deltaTime);

            if (remainingTime != (int)timer.Remaining)
            {
                UpdateText(remainingTime);
                remainingTime = (int)timer.Remaining;
            }

            yield return null;
        }

        player.transform.position = transform.position;

        player.EnableGravity(false);

        player.gameObject.SetActive(true);

        player.transform.DOKill();
        player.transform.localScale = Vector3.zero;
        player.transform
            .DOScale(player.InitialSize, 0.3f)
            .SetEase(Ease.OutBack)
            .OnComplete(() => player.EnableGravity(true));

        if (respawnCoroutine != null)
        {
            StopCoroutine(respawnCoroutine);
            respawnCoroutine = null;
        }

        canvas.enabled = false;
    }

    private void UpdateText(int value)
    {
        timerText.transform.localScale = Vector3.zero;

        timerText.text = value.ToString();

        timerText.transform.DOScale(Vector3.one, 0.2f)
                           .SetEase(Ease.OutBack);

    }
}
