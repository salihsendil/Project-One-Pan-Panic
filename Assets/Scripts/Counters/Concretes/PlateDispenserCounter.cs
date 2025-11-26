using UnityEngine;

[RequireComponent(typeof(PlateDispenserModule))]
public class PlateDispenserCounter : BaseCounter
{
    protected override void Awake()
    {
        base.Awake();
        TryGetComponent(out PlateDispenserModule plateDispenserModule);
        counterModules[0] = plateDispenserModule;
    }
}
