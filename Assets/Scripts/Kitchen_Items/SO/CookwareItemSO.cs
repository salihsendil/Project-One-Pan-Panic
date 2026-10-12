using UnityEngine;

[CreateAssetMenu(fileName = "New_Cookware_Item_SO", menuName = "Scriptable Objects/Kitchen Items/Containers/New Cookware Item SO")]
public class CookwareItemSO : ContainerItemSO
{
    [Header("Process")]
    public ProcessType ProcessType;
    public float ProcessTime;

    [Header("Burn Process")]
    public int DelayBeforeBurnDuration;
    public int BurnDuration;
    [SerializeField] private int totalBurnDuration;

    public int TotalBurnDuration => totalBurnDuration;

    private void OnValidate()
    {
        totalBurnDuration = DelayBeforeBurnDuration + BurnDuration;
    }
}