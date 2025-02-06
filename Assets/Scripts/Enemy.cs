using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public List<Transform> waypoints = new();

    [SerializeField] private float speed;
    public float Speed { get { return speed; } set { speed = value; } }
    [SerializeField] private float maxHealth = 5;
    private float health;

    [SerializeField] private Transform healthBar;

    private int currentWaypoint = 0;
    private List<IStatus> activeStatuses = new();
    [SerializeField] private Transform slowIcon;

    void Start()
    {

        GameObject waypointContainer = GameObject.FindGameObjectWithTag("Respawn");
        foreach (Transform child in waypointContainer.transform)
        {
            waypoints.Add(child);
        }

        if (healthBar == null)
        {
            healthBar = transform.Find("HP_Scaler");
        }

        health = maxHealth;
        UpdateHealth();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsNearWaypoint())
        {
            NextWaypoint();
        }

        Move();
        UpdateStatuses(Time.deltaTime);
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
        UpdateHealth();

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void UpdateHealth()
    {
        float hpPercentage = Mathf.Max(health / maxHealth, 0);
        Vector3 barScale = new Vector3(hpPercentage, 1, 1);

        healthBar.localScale = barScale;
    }

    public void ApplyStatuses(IStatus status)
    {
        foreach (IStatus activeStats in activeStatuses)
        {
            if (activeStats is SlowStatus slowStatus)
            {
                slowStatus.Reapply();
                return;
            }
        }

        activeStatuses.Add(status);
        status.Apply(this);
    }

    public void UpdateStatuses(float deltaTime)
    {
        foreach (var status in activeStatuses)
        {
            status.Tick(this, deltaTime);
        }

        if (activeStatuses.Any(status => status is SlowStatus))
        {
            slowIcon.gameObject.SetActive(true);
        }
        else
        {
            slowIcon.gameObject.SetActive(false);
        }
    }

    public void RemoveStatus(IStatus status)
    {
        activeStatuses.Remove(status);
    }
}
