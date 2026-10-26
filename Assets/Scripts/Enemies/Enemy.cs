using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    public int hitsToKill = 2;
    public int scoreValue = 100;

    [Header("Attack")]
    public EnemyProjectile projectilePrefab;
    public Transform firePoint;
    public float range = 35f;
    public float fireInterval = 3f;
    public float projectileSpeed = 22f;
    public float damage = 10f;
    public float aimSpread = 2.5f;

    [Header("Hit flash")]
    public Renderer bodyRenderer;
    public Color flashColor = Color.white;

    private int hitsTaken;
    private float nextShotTime;
    private Transform player;
    private Rigidbody playerBody;
    private Collider playerCollider;
    private MaterialPropertyBlock flashBlock;

    void Start()
    {
        nextShotTime = Time.time + Random.Range(0.5f, fireInterval);

        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
            playerBody = playerObject.GetComponent<Rigidbody>();
            playerCollider = playerObject.GetComponentInChildren<Collider>();
        }
    }

    void Update()
    {
        if (!LevelManager.Instance.IsPlaying || player == null) return;

        Vector3 toPlayer = player.position - transform.position;
        toPlayer.y = 0;
        if (toPlayer.sqrMagnitude > 0.01f)
        {
            transform.rotation = Quaternion.LookRotation(toPlayer);
        }

        bool playerPassed = Vector3.Dot(transform.position - player.position, player.forward) < 2;
        if (!playerPassed && toPlayer.magnitude <= range && Time.time >= nextShotTime)
        {
            ShootAtPlayer();
            nextShotTime = Time.time + fireInterval;
        }
    }

    private void ShootAtPlayer()
    {
        Vector3 target = playerCollider != null ? playerCollider.bounds.center : player.position;
        float flightTime = Vector3.Distance(firePoint.position, target) / projectileSpeed;
        if (playerBody != null)
        {
            target += playerBody.linearVelocity * flightTime;
        }
        target += Random.insideUnitSphere * aimSpread;

        Vector3 direction = (target - firePoint.position).normalized;
        EnemyProjectile projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.LookRotation(direction));
        projectile.Launch(direction * projectileSpeed, damage);
    }

    public void TakeDamage(float amount)
    {
        if (hitsTaken >= hitsToKill) return;

        hitsTaken++;
        if (hitsTaken >= hitsToKill)
        {
            LevelManager.Instance.AddScore(scoreValue);
            Destroy(gameObject);
            return;
        }

        if (bodyRenderer != null)
        {
            StartCoroutine(Flash());
        }
    }

    private IEnumerator Flash()
    {
        if (flashBlock == null)
        {
            flashBlock = new MaterialPropertyBlock();
        }
        flashBlock.SetColor("_BaseColor", flashColor);
        bodyRenderer.SetPropertyBlock(flashBlock);
        yield return new WaitForSeconds(0.1f);
        bodyRenderer.SetPropertyBlock(null);
    }
}
