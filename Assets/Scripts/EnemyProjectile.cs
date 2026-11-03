using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float lifetime = 5f;

    private float damage;
    private bool hasHit;

    public void Launch(Vector3 velocity, float projectileDamage)
    {
        damage = projectileDamage;
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.linearVelocity = velocity;
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasHit || other.isTrigger || other.GetComponentInParent<Enemy>() != null) return;
        hasHit = true;

        PlayerHealth playerHealth = other.GetComponentInParent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
