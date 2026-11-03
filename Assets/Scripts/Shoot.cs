using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{

    public Camera cam;
    public GameObject projectile;
    public Transform firePoint;
    public float minSpeed = 10;
    public float maxSpeed = 40;
    public float chargeTime = 1;
    public float minDamage = 10;
    public float maxDamage = 50;
    private float charge;
    private bool charging;
    private float timeToFire;
    public float fireRate = 4;
    void Update()
    {
        if (!LevelManager.Instance.IsPlaying || Mouse.current == null) return;

        if (Mouse.current.leftButton.wasPressedThisFrame && Time.time >= timeToFire)
        {
            charging = true;
        }

        if (charging)
        {
            charge = Mathf.Min(charge + Time.deltaTime / chargeTime, 1);
            HUD.Instance.SetCharge(charge);

            if (Mouse.current.leftButton.wasReleasedThisFrame)
            {
                timeToFire = Time.time + 1 / fireRate;
                ShootProjectile(Mathf.Lerp(minSpeed, maxSpeed, charge), Mathf.Lerp(minDamage, maxDamage, charge));
                charging = false;
                charge = 0;
                HUD.Instance.SetCharge(0);
            }
        }
    }

    private void ShootProjectile(float speed, float damage)
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Vector3 destination = ray.GetPoint(1000);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            destination = hit.point;
        }
        Vector3 direction = (destination - firePoint.position).normalized;
        InstantiateProjectile(direction, speed, damage);
    }

    private void InstantiateProjectile(Vector3 direction, float speed, float damage)
    {
        GameObject projectileObj = Instantiate(projectile, firePoint.position, Quaternion.LookRotation(direction));
        projectileObj.GetComponent<Rigidbody>().linearVelocity = direction * speed;
        projectileObj.GetComponent<Projectile>().damage = damage;
        Destroy(projectileObj, 5f);
    }
}
