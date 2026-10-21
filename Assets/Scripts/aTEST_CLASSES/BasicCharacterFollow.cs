using UnityEngine;

public class BasicCharacterFollow : MonoBehaviour
{
    [SerializeField] private Camera followCam;
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 positionOffset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 camPos = player.transform.position + positionOffset;
        followCam.transform.position = camPos;

        followCam.transform.LookAt(player.transform, Vector3.up);
    }
}
