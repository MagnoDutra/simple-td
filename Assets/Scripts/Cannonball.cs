using UnityEngine;

public class Cannonball : Bullet
{
    [SerializeField] private float radiusDamage;
    [SerializeField] private GameObject explosionVFX;

    protected override void DoDamage()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, radiusDamage);

        foreach (Collider2D enemy in enemies)
        {
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            if (enemyScript != null)
            {
                enemyScript.TakeDamage(damage);
            }
        }

        Instantiate(explosionVFX, transform.position, Quaternion.Euler(90, 0, 0));
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radiusDamage);
    }
}
