using UnityEngine;

[RequireComponent(typeof(ItemProcessDisplay))]
public class CuttableProcess : BaseItemProcess
{
    private void Awake()
    {
        processDisplayer = GetComponent<ItemProcessDisplay>();
    }
}
