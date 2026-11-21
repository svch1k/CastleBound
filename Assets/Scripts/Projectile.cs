using UnityEngine;

public class Projectile : MonoBehaviour
{
    [HideInInspector] public float damage;
    private bool hasHit;

    public AudioClipGroup hitSound;

    private void OnCollisionEnter(Collision collision)
    {
        if (hasHit) return;

        if(collision.gameObject.tag != "Player" && collision.gameObject.tag != "Arrow")
        {
            hasHit = true;
            AudioSourcePool.Instance.Play(hitSound);
            Enemy enemy = collision.collider.GetComponentInParent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }

}
