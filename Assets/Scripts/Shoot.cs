using System;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    public Camera cam;
    private Vector3 destination;
    public GameObject projectile;
    public Transform firePoint;
    public float projectileSpeed = 30;
    private float timeToFire;
    public float fireRate = 4;
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= timeToFire)
        {
            timeToFire = Time.time + 1 / fireRate;
            ShootProjectile();
        }
    }

    private void ShootProjectile()
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
        InstantiateProjectile(direction);
    }

    private void InstantiateProjectile(Vector3 direction)
    {
        var projectileObj = Instantiate(projectile, firePoint.position, Quaternion.LookRotation(direction)) as GameObject;
        projectileObj.GetComponent<Rigidbody>().linearVelocity = direction * projectileSpeed;
    }
}
