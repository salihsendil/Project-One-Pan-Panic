using System.Collections.Generic;
using UnityEngine;

public class BillboardManager : MonoBehaviour
{
    private static Transform mainCameraTransform => Camera.main.transform;
    private HashSet<ContainerIconBillboarding> containerInUseList = new HashSet<ContainerIconBillboarding>();
    public void RegisterContainerToBillBoarding(ContainerIconBillboarding container) => containerInUseList.Add(container);
    public void UnRegisterContainerToBillBoarding(ContainerIconBillboarding container) => containerInUseList.Remove(container);

    void Update()
    {
        foreach (var container in containerInUseList)
        {
            container.ContainerCanvas.transform.rotation = Quaternion.LookRotation(mainCameraTransform.forward);
        }
    }

}
