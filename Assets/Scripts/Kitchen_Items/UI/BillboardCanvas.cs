using System.Collections.Generic;
using UnityEngine;

public class BillboardCanvas : MonoBehaviour
{
    //Canvas
    [SerializeField] private Canvas canvas;

    //Canvas Groups
    private HashSet<CanvasGroup> activeGroups = new();

    //Main Camera
    private Transform mainCamTransform;

    private void Awake()
    {
        if (Camera.main != null)
            mainCamTransform = Camera.main.transform;
    }

    private void LateUpdate()
    {
        if (activeGroups.Count > 0)
        {
            Quaternion targetRotation = mainCamTransform.rotation;

            if (Quaternion.Angle(canvas.transform.rotation, targetRotation) > 1f)
            {
                canvas.transform.rotation = targetRotation;
            }
        }
    }

    public void Register(CanvasGroup canvasGroup)
    {
        activeGroups.Add(canvasGroup);
    }

    public void Unregister(CanvasGroup canvasGroup)
    {
        if (activeGroups.Contains(canvasGroup))
            activeGroups.Remove(canvasGroup);
    }
}
