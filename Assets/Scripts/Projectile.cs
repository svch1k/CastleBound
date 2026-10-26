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
            IDamageable target = collision.collider.GetComponentInParent<IDamageable>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }

}
