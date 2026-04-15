using UnityEngine;

public class ItemCanvasBillboardHandler : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvas;

    private Quaternion targetRotation;

    private void Awake()
    {
        targetRotation = Camera.main.transform.rotation;
        canvas.transform.rotation = targetRotation;
    }

    private void Update()
    {

        if (Quaternion.Angle(transform.rotation, targetRotation) > 1.0f)
        {
            canvas.transform.rotation = targetRotation;
        }

    }
}
