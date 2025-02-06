using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected float speed = 1f;
    [SerializeField] protected int damage;
    protected Enemy target;

    // Update is called once per frame
    protected virtual void Update()
    {
        if (target == null) return;

        FaceEnemy();

        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.transform.position) < 0.1f)
        {
            DoDamage();
        }
    }

    public void SetupBullet(Enemy target)
    {
        this.target = target;
    }

    protected virtual void FaceEnemy()
    {
        Vector3 direction = target.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    protected virtual void DoDamage()
    {
        target.TakeDamage(damage);
        Destroy(gameObject);
    }
}
