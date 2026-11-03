using UnityEngine;

public class Projectile : MonoBehaviour
{
    [HideInInspector] public float damage;
    private bool hasHit;

    private void OnCollisionEnter(Collision collision)
    {
        if (hasHit) return;

        if(collision.gameObject.tag != "Player" && collision.gameObject.tag != "Arrow")
        {
            hasHit = true;
            Enemy enemy = collision.collider.GetComponentInParent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }

}
