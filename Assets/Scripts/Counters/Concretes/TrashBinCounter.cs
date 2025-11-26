using UnityEngine;

[RequireComponent(typeof(TrashModule))]
public class TrashBinCounter : BaseCounter
{

    protected override void Awake()
    {
        TryGetComponent(out TrashModule trashModule);
        counterModules[0] = trashModule;
    }
}
