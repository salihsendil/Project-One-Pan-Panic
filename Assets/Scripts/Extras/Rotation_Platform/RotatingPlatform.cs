using DG.Tweening;
using System.Collections;
using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform visualTransform; //For Tween Only!!

    [Header("Rotation Settings")]
    [SerializeField] private float rotationAmount = 90f;
    [SerializeField] private float rotationDelay = 3f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float rotationThreshold = 1f;

    [Header("Shake Settings")]
    [SerializeField] private float shakeDuration = 3f;
    [SerializeField] private float shakeStrength = 0.1f;
    [SerializeField] private int shakeVibrato = 100;
    [SerializeField] private float shakeRandomness = 90f;

    private WaitForSeconds delay;
    private WaitForFixedUpdate waitForFixedUpdate;
    private Tween shakeTween;
    private Coroutine rotationCoroutine;

    private void Start()
    {
        shakeTween = visualTransform.DOShakePosition(
            shakeDuration,
            shakeStrength,
            shakeVibrato,
            shakeRandomness).SetAutoKill(false).Pause();

        delay = new(rotationDelay);
        waitForFixedUpdate = new();
        rotationCoroutine = StartCoroutine(RotatePlatform());
    }


    private void OnDestroy()
    {
        shakeTween?.Kill();

        if (rotationCoroutine != null)
        {
            StopCoroutine(rotationCoroutine);
            rotationCoroutine = null;
        }
    }

    private IEnumerator RotatePlatform()
    {
        while (true)
        {
            yield return delay;

            float targetYAngle = rb.rotation.eulerAngles.y + rotationAmount;

            shakeTween.Restart();

            while (Mathf.Abs(Mathf.DeltaAngle(rb.rotation.eulerAngles.y, targetYAngle)) > rotationThreshold)
            {
                float currentY = rb.rotation.eulerAngles.y;
                float nextY = Mathf.MoveTowardsAngle(currentY, targetYAngle, rotationSpeed * Time.fixedDeltaTime);

                Quaternion targetRot = Quaternion.Euler(rb.rotation.eulerAngles.x, nextY, rb.rotation.eulerAngles.z);
                rb.MoveRotation(targetRot);

                yield return waitForFixedUpdate;
            }

            rb.MoveRotation(Quaternion.Euler(rb.rotation.eulerAngles.x, targetYAngle, rb.rotation.eulerAngles.z));
        }
    }
}