using System;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    public Camera cam;
    private Vector3 destination;
    public GameObject projectile;
    public Transform firePoint;
    public float minSpeed = 10;
    public float maxSpeed = 40;
    public float chargeTime = 1;
    private float charge;
    private bool charging;
    private float timeToFire;
    public float fireRate = 4;
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= timeToFire)
        {
            charging = true;
        }

        if (charging)
        {
            charge = Mathf.Min(charge + Time.deltaTime / chargeTime, 1);
            HUD.Instance.SetCharge(charge);

            if (Input.GetButtonUp("Fire1"))
            {
                timeToFire = Time.time + 1 / fireRate;
                ShootProjectile(Mathf.Lerp(minSpeed, maxSpeed, charge));
                charging = false;
                charge = 0;
                HUD.Instance.SetCharge(0);
            }
        }
    }

    private void ShootProjectile(float speed)
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
         if(Physics.Raycast(ray, out hit))
        {
            destination = hit.point;
        }
        else
        {
            destination = ray.GetPoint(1000);
        }
        Vector3 direction = (destination - firePoint.position).normalized;
        InstantiateProjectile(direction, speed);
    }

    private void InstantiateProjectile(Vector3 direction, float speed)
    {
        var projectileObj = Instantiate(projectile, firePoint.position, Quaternion.LookRotation(direction)) as GameObject;
        projectileObj.GetComponent<Rigidbody>().linearVelocity = direction * speed;
    }
}
