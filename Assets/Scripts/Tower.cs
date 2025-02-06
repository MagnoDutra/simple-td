using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private float attackRange = 5f;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float attackCooldown = 1f;

    private float attackCountdown;
    private Queue<Enemy> targetsInRange = new();
    private Enemy currentTarget;

    private void Start()
    {
        GetComponent<CircleCollider2D>().radius = attackRange;
    }

    void Update()
    {
        if (currentTarget == null || !targetsInRange.Contains(currentTarget))
        {
            SelectTarget();
        }

        if (CanAttack() && currentTarget != null)
        {
            Attack();
        }
    }

    private bool CanAttack()
    {
        attackCountdown = Mathf.Max(attackCountdown - Time.deltaTime, 0);

        return attackCountdown <= 0;
    }

    private void SelectTarget()
    {
        while (targetsInRange.Count > 0)
        {
            Enemy nextEnemy = targetsInRange.Peek();

            if (nextEnemy == null)
            {
                targetsInRange.Dequeue();
            }
            else
            {
                currentTarget = nextEnemy;
                break;
            }
        }
    }

    public void Attack()
    {
        Bullet bulletInstance = Instantiate(bulletPrefab, transform.position, Quaternion.identity).GetComponent<Bullet>();
        bulletInstance.SetupBullet(currentTarget);

        attackCountdown = attackCooldown;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy = other.GetComponent<Enemy>();

        if (enemy != null)
        {
            targetsInRange.Enqueue(enemy);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null && targetsInRange.Contains(enemy))
        {
            targetsInRange = new Queue<Enemy>(targetsInRange.Where(e => e != enemy));
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
