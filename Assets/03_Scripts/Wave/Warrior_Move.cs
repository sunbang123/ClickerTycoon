using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior_Move : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 3f;
    public Transform[] waypoints;
    private int currentWaypointIndex = 0;
    private Rigidbody2D rb;
    private Vector2 movement;

    private void Awake()
    {
        waypoints = Warrior_Spawner.instance.setWaypoints;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        MoveTowardsWaypoint();
    }

    void MoveTowardsWaypoint()
    {
        if (currentWaypointIndex >= waypoints.Length) return;
        Transform targetWaypoint = waypoints[currentWaypointIndex];
        Vector2 direction = (targetWaypoint.position - transform.position).normalized;
        movement = direction * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);
        if (Vector2.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            currentWaypointIndex = currentWaypointIndex + 1;
        }
    }
}
