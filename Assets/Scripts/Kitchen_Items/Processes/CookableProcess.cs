using UnityEngine;

[RequireComponent(typeof(ItemProcessDisplay))]
public class CookableProcess : BaseItemProcess
{
    private void Awake()
    {
        processDisplayer = GetComponent<ItemProcessDisplay>();
    }
}
