using System.Threading.Tasks;
using Unity.Cinemachine;
using UnityEngine;


// This class smoothly moves the MainCam when the game starts.
// It requires a CinemachineCamera.


public class CameraIntroController : MonoBehaviour
{
    [SerializeField] private int introDelay = 150;
    [SerializeField] private CinemachineCamera introCam;

    void Start()
    {
        BlendIntroToMainCam();
    }

    private async void BlendIntroToMainCam()
    {
        await Task.Delay(introDelay);
        introCam.gameObject.SetActive(false);
    }
}
