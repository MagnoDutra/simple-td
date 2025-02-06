using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private int damage;
    private Enemy target;

    // Update is called once per frame
    void Update()
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

    private void FaceEnemy()
    {
        Vector3 direction = target.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void DoDamage()
    {
        target.TakeDamage(damage);
        Destroy(gameObject);
    }
}
