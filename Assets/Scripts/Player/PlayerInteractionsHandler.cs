using UnityEngine;

public class PlayerInteractionsHandler : MonoBehaviour
{
    void Start()
    {

    }
    void Update()
    {

    }

    private void TryInteract()
    {
        //send ray when press some button
        //we need to send ray when we have smthng on my hand
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Debug.DrawLine(transform.position, transform.position + transform.forward * 2f, Color.green);
        }
    }

}
