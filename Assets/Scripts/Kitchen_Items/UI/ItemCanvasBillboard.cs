using UnityEngine;

public class ItemCanvasBillboard : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;

    private Quaternion targetRotation;

    private void Awake()
    {
        targetRotation = Camera.main.transform.rotation;
        canvasGroup.transform.rotation = targetRotation;
    }

    private void Update()
    {
        if (canvasGroup.alpha == 1)
        {
            if (Quaternion.Angle(transform.rotation, targetRotation) > 1.0f)
            {
                canvasGroup.transform.rotation = targetRotation;
            }
        }
    }
}
