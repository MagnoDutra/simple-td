using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public List<Transform> waypoints = new();

    [SerializeField] private float speed;
    [SerializeField] private int health;

    private int currentWaypoint = 0;

    void Start()
    {
        GameObject waypointContainer = GameObject.FindGameObjectWithTag("Respawn");
        foreach (Transform child in waypointContainer.transform)
        {
            waypoints.Add(child);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (IsNearWaypoint())
        {
            NextWaypoint();
        }

        Move();
    }

    bool IsNearWaypoint()
    {
        return Vector3.Distance(transform.position, waypoints[currentWaypoint].position) < 0.05f;
    }

    void NextWaypoint()
    {
        currentWaypoint++;
        currentWaypoint = Mathf.Clamp(currentWaypoint, 0, waypoints.Count - 1);
    }

    private void Move()
    {
        transform.position =
            Vector3.MoveTowards(transform.position, waypoints[currentWaypoint].position, speed * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);
        }

    }
}
