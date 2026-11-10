using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Waypoint")]
    [SerializeField] private Rigidbody rb;

    [Header("Waypoint")]
    [SerializeField] private List<Transform> waypoints = new();
    [SerializeField] private Transform targetWaypoint;
    [SerializeField] private int targetWaypointIndex = 0;

    [Header("Movement")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float distanceTreshold = 0.1f;

    [Header("Coroutine")]
    [SerializeField] private float waitDuration = 1f;
    private WaitForSeconds waitDelay;
    private WaitForFixedUpdate waitFixedUpdate;
    private Coroutine coroutine;

    private void Start()
    {
        if (waypoints != null && waypoints.Count >= 2)
        {
            targetWaypoint = waypoints[targetWaypointIndex];
        }

        waitDelay = new(waitDuration);
        waitFixedUpdate = new();
        coroutine = StartCoroutine(Move());
    }

    private void OnDisable()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
    }

    private IEnumerator Move()
    {
        while (true)
        {
            float distance = Vector3.Distance(targetWaypoint.position, rb.position);
            if (distance <= distanceTreshold)
            {
                targetWaypointIndex = targetWaypointIndex == waypoints.Count - 1 ? 0 : targetWaypointIndex + 1;
                targetWaypoint = waypoints[targetWaypointIndex];
                yield return waitDelay;
            }

            Vector3 targetVector = Vector3.MoveTowards(transform.position, targetWaypoint.position, speed * Time.deltaTime);
            rb.MovePosition(targetVector);

            yield return waitFixedUpdate;
        }
    }
}